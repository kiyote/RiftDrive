using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.Extensions.Options;
using RiftDrive.Common.Model;
using RiftDrive.Server.Services.AWS;
using RiftDrive.Server.Services.PitBoss.Model;

namespace RiftDrive.Server.Services.PitBoss {

	[SuppressMessage( "Performance", "CA1812", Justification = "Dependency Injection" )]
	internal sealed class GameRepository : IGameRepository {

		private readonly IGameRepository _repo;
		private readonly DynamoDbContext<GameRepository> _context;
		private readonly GameOptions _options;
		private readonly DynamoDBOperationConfig _config;
		private readonly DynamoDBOperationConfig _searchConfig;

		public GameRepository(
			DynamoDbContext<GameRepository> context,
			IOptions<GameOptions> options
		) {
			_options = options.Value;
			_context = context;

			_config = new DynamoDBOperationConfig() {
				OverrideTableName = _options.TableName
			};
			_searchConfig = new DynamoDBOperationConfig() {
				OverrideTableName = _options.TableName,
				IndexName = "GSI"
			};

			_repo = this;
		}


		async Task<IEnumerable<Game>> IGameRepository.GetUserGamesAsync( Id<User> userId ) {
			AsyncSearch<PlayerRecord> query = _context.Context.QueryAsync<PlayerRecord>(
				PlayerRecord.GetKey( userId.Value ),
				QueryOperator.BeginsWith,
				new List<object>() { GameRecord.ItemType },
				_searchConfig
			);

			List<PlayerRecord> playerRecords = await query.GetRemainingAsync().ConfigureAwait( false );

			BatchGet<GameRecord> batchGet = _context.Context.CreateBatchGet<GameRecord>( _config );
			foreach( PlayerRecord record in playerRecords ) {
				batchGet.AddKey( GameRecord.GetKey( record.GameId ), GameRecord.GetKey( record.GameId ) );
			}
			await batchGet.ExecuteAsync().ConfigureAwait( false );

			return batchGet.Results.Select( r => ToGame( r ) );
		}

		async Task<IEnumerable<Game>> IGameRepository.GetAwaitingPlayersGamesAsync() {
			AsyncSearch<GameStateRecord> query = _context.Context.QueryAsync<GameStateRecord>(
				GameStateRecord.GetKey( GameState.WaitingForPlayers.ToString() ),
				QueryOperator.BeginsWith,
				new List<object>() { GameRecord.ItemType },
				_searchConfig
			);

			List<GameStateRecord> stateRecords = await query.GetRemainingAsync().ConfigureAwait( false );

			BatchGet<GameRecord> batchGet = _context.Context.CreateBatchGet<GameRecord>( _config );
			foreach( GameStateRecord record in stateRecords ) {
				batchGet.AddKey( GameRecord.GetKey( record.GameId ), GameRecord.GetKey( record.GameId ) );
			}
			await batchGet.ExecuteAsync().ConfigureAwait( false );

			return batchGet.Results.Select( r => ToGame( r ) );
		}

		async Task<Game> IGameRepository.CreateGameAsync( Id<Game> gameId, string name, DateTime createdOn, GameState state ) {
			var gameRecord = new GameRecord {
				GameId = gameId.Value,
				Name = name,
				CreatedOn = createdOn.ToUniversalTime()
			};
			await _context.Context.SaveAsync( gameRecord, _config ).ConfigureAwait( false );

			var gameStateRecord = new GameStateRecord {
				GameId = gameId.Value,
				State = state.ToString(),
				CreatedOn = createdOn.ToUniversalTime()
			};
			await _context.Context.SaveAsync( gameStateRecord, _config ).ConfigureAwait( false );

			// We return the loaded record so that we have round-tripped all the values
			// such as a DateTime that might be very subtly altered
			return await _repo.GetGameAsync( gameId ).ConfigureAwait( false );
		}

		async Task IGameRepository.DeleteGameAsync( Id<Game> gameId ) {
			AsyncSearch<GameStateRecord> stateQuery = _context.Context.QueryAsync<GameStateRecord>(
				GameRecord.GetKey( gameId.Value ),
				QueryOperator.BeginsWith,
				new List<object>() {
					GameStateRecord.ItemType
				},
				_config );

			IEnumerable<GameStateRecord> stateRecords = await stateQuery.GetRemainingAsync().ConfigureAwait( false );
			foreach ( GameStateRecord stateRecord in stateRecords) {
				await _context.Context.DeleteAsync<GameStateRecord>(
					GameRecord.GetKey( gameId.Value ),
					GameStateRecord.GetKey( stateRecord.State ),
					_config ).ConfigureAwait( false );
			}

			AsyncSearch<PlayerRecord> playerQuery = _context.Context.QueryAsync<PlayerRecord>(
				GameRecord.GetKey( gameId.Value ),
				QueryOperator.BeginsWith,
				new List<object>() {
					PlayerRecord.ItemType
				},
				_config );

			IEnumerable<PlayerRecord> playerRecords = await playerQuery.GetRemainingAsync().ConfigureAwait( false );
			foreach( PlayerRecord playerRecord in playerRecords ) {
				await _context.Context.DeleteAsync<PlayerRecord>(
					GameRecord.GetKey( gameId.Value ),
					PlayerRecord.GetKey( playerRecord.UserId ),
					_config ).ConfigureAwait( false );
			}

			await _context.Context.DeleteAsync<GameRecord>(
				GameRecord.GetKey( gameId.Value ),
				GameRecord.GetKey( gameId.Value ),
				_config
			).ConfigureAwait( false );
		}

		async Task<Game> IGameRepository.GetGameAsync( Id<Game> gameId ) {
			GameRecord gameRecord = await _context.Context.LoadAsync<GameRecord>(
				GameRecord.GetKey( gameId.Value ),
				GameRecord.GetKey( gameId.Value ),
				_config
			).ConfigureAwait( false );

			if( gameRecord == default ) {
				return default;
			}

			return ToGame( gameRecord );
		}

		async Task IGameRepository.StartGameAsync( Id<Game> gameId ) {
			GameStateRecord gameStateRecord = await _context.Context.LoadAsync<GameStateRecord>(
				GameRecord.GetKey( gameId.Value ),
				GameStateRecord.GetKey( GameState.WaitingForPlayers.ToString() ),
				_config
			).ConfigureAwait( false );

			if( gameStateRecord == default ) {
				throw new ArgumentException( "Invalid gameId", nameof(gameId));
			}

			gameStateRecord.State = GameState.Active.ToString();
			await _context.Context.SaveAsync( gameStateRecord ).ConfigureAwait( false );
		}

		async Task IGameRepository.JoinGameAsync( Id<Game> gameId, Id<User> userId, string name, DateTime createdOn ) {
			PlayerRecord playerRecord = new PlayerRecord() {
				GameId = gameId.Value,
				UserId = userId.Value,
				Name = name,
				CreatedOn = createdOn
			};

			await _context.Context.SaveAsync( playerRecord, _config ).ConfigureAwait( false );	
		}

		/*
		private async Task AddToStatistic( string statistic, int amount ) {
			var request = new UpdateItemRequest {
				TableName = "RiftDrive-Development",
				Key = new Dictionary<string, AttributeValue>() {
					{ "PK", new AttributeValue("Statistics") },
					{ "SK", new AttributeValue("Game") }
				},
				UpdateExpression = $"SET {statistic} = if_not_exists( {statistic}, :zero ) + :gamecount",
				ExpressionAttributeValues = new Dictionary<string, AttributeValue>() {
					{ ":zero", new AttributeValue() {
						N = "0"
					} },
					{ ":gamecount", new AttributeValue() {
						N = amount.ToString( CultureInfo.InvariantCulture )
					} }
				}
			};
			await _context.Client.UpdateItemAsync( request ).ConfigureAwait( false );
		}
		*/

		private static Game ToGame( GameRecord r ) {
			return new Game(
				new Id<Game>( r.GameId ),
				r.Name,
				r.CreatedOn );
		}
	}
}
