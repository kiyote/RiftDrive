/*
 * Copyright 2018-2019 Todd Lang
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
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using RiftDrive.Server.Repository.DynamoDb.Model;
using RiftDrive.Shared.Model;

namespace RiftDrive.Server.Repository.DynamoDb {
	internal sealed class MissionRepository: IMissionRepository {

		private readonly IAmazonDynamoDB _client;
		private readonly IDynamoDBContext _context;

		public MissionRepository(
			IAmazonDynamoDB client,
			IDynamoDBContext context
		) {
			_client = client;
			_context = context;
		}

		async Task<Mission> IMissionRepository.GetByGameId( Id<Game> gameId ) {
			AsyncSearch<GameMissionRecord> query = _context.QueryAsync<GameMissionRecord>(
				GameRecord.GetKey( gameId.Value ),
				QueryOperator.BeginsWith,
				new List<object>() { MissionRecord.ItemType } );

			List<GameMissionRecord> records = await query.GetRemainingAsync();
			GameMissionRecord record = records.FirstOrDefault();

			if (record == default) {
				return default;
			}

			MissionRecord missionRecord = await _context.LoadAsync<MissionRecord>(
				MissionRecord.GetKey( record.MissionId ),
				MissionRecord.GetKey( record.MissionId ) );

			return ToMission( gameId, missionRecord );
		}

		async Task<Mission> IMissionRepository.Create( Id<Game> gameId, Id<Mission> missionId, DateTime createdOn ) {
			MissionRecord missionRecord = new MissionRecord() {
				MissionId = missionId.Value,
				CreatedOn = createdOn.ToUniversalTime()
			};

			await _context.SaveAsync( missionRecord );

			GameMissionRecord gameMissionRecord = new GameMissionRecord() {
				GameId = gameId.Value,
				MissionId = missionId.Value
			};

			await _context.SaveAsync( gameMissionRecord );

			return ToMission( gameId, missionRecord );
		}

		private static Mission ToMission( Id<Game> gameId, MissionRecord r ) {
			return new Mission(
				gameId,
				new Id<Mission>( r.MissionId ) );
		}
	}
}
