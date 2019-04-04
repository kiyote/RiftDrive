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
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using RiftDrive.Server.Model;
using RiftDrive.Server.Repository.DynamoDb.Model;
using RiftDrive.Shared;

namespace RiftDrive.Server.Repository.DynamoDb {
	internal sealed class MothershipRepository : IMothershipRepository {

		private readonly IDynamoDBContext _context;

		public MothershipRepository(
			IDynamoDBContext context
		) {
			_context = context;
		}

		async Task<Mothership> IMothershipRepository.Create( Id<Game> gameId, Id<Mothership> mothershipId, string name, DateTime createdOn ) {
			MothershipRecord record = new MothershipRecord {
				GameId = gameId.Value,
				MothershipId = mothershipId.Value,
				Name = name,
				CreatedOn = createdOn.ToUniversalTime()
			};

			await _context.SaveAsync( record );

			return ToMothership( record );
		}

		async Task IMothershipRepository.Delete(Id<Game> gameId ) {
			AsyncSearch<MothershipRecord> query = _context.QueryAsync<MothershipRecord>(
				GameRecord.GetKey( gameId.Value ),
				QueryOperator.BeginsWith,
				new List<object> { MothershipRecord.ItemType } );

			List<MothershipRecord> ships = await query.GetRemainingAsync();
			foreach (var ship in ships) {
				await _context.DeleteAsync<MothershipRecord>( GameRecord.GetKey( gameId.Value ), MothershipRecord.GetKey( ship.MothershipId ) );
			}
		}

		private Mothership ToMothership( MothershipRecord r ) {
			return new Mothership(
				new Id<Mothership>( r.MothershipId ),
				new Id<Game>( r.GameId ),
				r.Name);
		}
	}
}
