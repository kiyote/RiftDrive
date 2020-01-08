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
using RiftDrive.Shared.Model;
using RiftDrive.Server.Model;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using RiftDrive.Server.Repository.DynamoDb.Model;
using Amazon.DynamoDBv2.DocumentModel;

namespace RiftDrive.Server.Repository.DynamoDb {
	internal sealed class PlayerRepository : IPlayerRepository {

		private readonly IDynamoDBContext _context;

		public PlayerRepository(
			IDynamoDBContext context
		) {
			_context = context;
		}

		async Task<Player> IPlayerRepository.Create( Id<Game> gameId, Id<Player> playerId, Id<User> userId, string name, DateTime createdOn ) {
			var gameUserRecord = new GameUserRecord {
				PlayerId = playerId.Value,
				GameId = gameId.Value,
				UserId = userId.Value,
				CreatedOn = createdOn
			};

			await _context.SaveAsync( gameUserRecord );

			var playerRecord = new PlayerRecord {
				GameId = gameId.Value,
				PlayerId = playerId.Value,
				UserId = userId.Value,
				Name = name,
				CreatedOn = createdOn
			};

			await _context.SaveAsync( playerRecord );

			return new Player(
				playerId,
				gameId,
				userId,
				name,
				createdOn );
		}

		async Task IPlayerRepository.Delete( Id<Game> gameId, Id<Player> playerId ) {
			Player? player = await GetById( gameId, playerId );

			if (player == default) {
				return;
			}

			await _context.DeleteAsync<PlayerRecord>( GameRecord.GetKey( gameId.Value ), PlayerRecord.GetKey( playerId.Value ) );
			await _context.DeleteAsync<GameUserRecord>( GameRecord.GetKey( gameId.Value ), UserRecord.GetKey( player.UserId.Value ) );
		}

		async Task<Player?> IPlayerRepository.Get( Id<Game> gameId, Id<Player> playerId ) {
			return await GetById( gameId, playerId );
		}

		private async Task<Player?> GetById( Id<Game> gameId, Id<Player> playerId ) {
			PlayerRecord playerRecord = await _context.LoadAsync<PlayerRecord>(
				GameRecord.GetKey( gameId.Value ),
				PlayerRecord.GetKey( playerId.Value ) );

			if (playerRecord == default) {
				return default;
			}

			return new Player(
				playerId,
				gameId,
				new Id<User>( playerRecord.UserId ),
				playerRecord.Name,
				playerRecord.CreatedOn );
		}

		async Task<IEnumerable<Player>> IPlayerRepository.GetPlayers( Id<Game> gameId ) {
			AsyncSearch<PlayerRecord> query = _context.QueryAsync<PlayerRecord>( GameRecord.GetKey( gameId.Value ),
				QueryOperator.BeginsWith,
				new List<object>() { PlayerRecord.ItemType } );

			List<PlayerRecord> results = await query.GetRemainingAsync();

			return results.Select( r => new Player(
				new Id<Player>( r.PlayerId ),
				new Id<Game>( r.GameId ),
				new Id<User>( r.UserId ),
				r.Name,
				r.CreatedOn ) );
		}
	}
}
