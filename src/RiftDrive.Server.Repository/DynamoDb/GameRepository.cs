/*
 * Copyright 2018-2020 Todd Lang
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using RiftDrive.Server.Model;
using RiftDrive.Server.Repository.DynamoDb.Model;
using RiftDrive.Shared.Model;

namespace RiftDrive.Server.Repository.DynamoDb {
	internal sealed class GameRepository : IGameRepository {

		private readonly IAmazonDynamoDB _client;
		private readonly IDynamoDBContext _context;

		public GameRepository(
			IAmazonDynamoDB client,
			IDynamoDBContext context
		) {
			_client = client;
			_context = context;
		}

		async Task<Game> IGameRepository.Create(
			Id<Game> gameId,
			string name,
			DateTime createdOn
		) {
			var gameRecord = new GameRecord {
				GameId = gameId.Value,
				Name = name,
				CreatedOn = createdOn.ToUniversalTime(),
				State = GameState.WaitingForPlayers.ToString()
			};
			await _context.SaveAsync( gameRecord );

			await AddStatistic( "TotalCount", 1 );

			return ToGame( gameRecord );
		}

		async Task IGameRepository.Delete( Id<Game> gameId ) {
			await _context.DeleteAsync<GameRecord>( GameRecord.GetKey( gameId.Value ), GameRecord.GetKey( gameId.Value ) );

			// Remove mission records
			AsyncSearch<GameMissionRecord> missionQuery = _context.QueryAsync<GameMissionRecord>(
				GameRecord.GetKey( gameId.Value ),
				QueryOperator.BeginsWith,
				new List<object>() {
					MissionRecord.ItemType
				} );

			BatchWrite<GameMissionRecord> batch = _context.CreateBatchWrite<GameMissionRecord>();
			List<GameMissionRecord> missionRecords = await missionQuery.GetRemainingAsync();
			foreach( GameMissionRecord r in missionRecords ) {
				batch.AddDeleteKey( r.PK, r.SK );
			}
			await _context.ExecuteBatchWriteAsync( new BatchWrite[] { batch } );

			// Finally, update the statistics
			await AddStatistic( "TotalCount", -1 );
		}

		async Task<IEnumerable<Game>> IGameRepository.GetGames( Id<User> userId ) {
			AsyncSearch<PlayerRecord> query = _context.QueryAsync<PlayerRecord>(
				UserRecord.GetKey( userId.Value ),
				QueryOperator.BeginsWith,
				new List<object>() { GameRecord.ItemType },
				new DynamoDBOperationConfig() {
					IndexName = "GSI"
				}
			);

			List<PlayerRecord> playerRecords = await query.GetRemainingAsync();

			BatchGet<GameRecord> batchGet = _context.CreateBatchGet<GameRecord>();
			foreach( PlayerRecord record in playerRecords ) {
				batchGet.AddKey( GameRecord.GetKey( record.GameId ), GameRecord.GetKey( record.GameId ) );
			}
			await batchGet.ExecuteAsync();

			return batchGet.Results.Select( r => ToGame( r ) );
		}

		async Task<Game?> IGameRepository.GetGame( Id<Game> gameId ) {
			GameRecord gameRecord = await _context.LoadAsync<GameRecord>( GameRecord.GetKey( gameId.Value ), GameRecord.GetKey( gameId.Value ) );

			if (gameRecord == default) {
				throw new ArgumentException();
			}

			return ToGame( gameRecord );
		}

		async Task<Game> IGameRepository.StartGame( Id<Game> gameId ) {
			GameRecord gameRecord = await _context.LoadAsync<GameRecord>( GameRecord.GetKey( gameId.Value ), GameRecord.GetKey( gameId.Value ) );

			if (gameRecord == default) {
				throw new ArgumentException();
			}

			gameRecord.State = GameState.Active.ToString();
			await _context.SaveAsync( gameRecord );

			return ToGame( gameRecord );
		}

		private async Task AddStatistic( string statistic, int amount ) {
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
						N = amount.ToString()
					} }
				}
			};
			await _client.UpdateItemAsync( request );
		}

		private static Game ToGame( GameRecord r ) {
			return new Game(
				new Id<Game>( r.GameId ),
				r.Name,
				r.CreatedOn,
				(GameState)Enum.Parse( typeof( GameState ), r.State ) );
		}
	}
}
