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

		async Task<Mothership> IMothershipRepository.CreateMothership(
			Id<Game> gameId,
			Id<Mothership> mothershipId,
			string name,
			int availableCrew,
			int remainingFuel,
			DateTime createdOn
		) {
			MothershipRecord record = new MothershipRecord {
				GameId = gameId.Value,
				MothershipId = mothershipId.Value,
				Name = name,
				CreatedOn = createdOn.ToUniversalTime(),
				AvailableCrew = availableCrew,
				RemainingFuel = remainingFuel
			};

			await _context.SaveAsync( record );

			return ToMothership( record );
		}

		async Task IMothershipRepository.DeleteMothership(
			Id<Game> gameId
		) {
			AsyncSearch<MothershipRecord> query = _context.QueryAsync<MothershipRecord>(
				GameRecord.GetKey( gameId.Value ),
				QueryOperator.BeginsWith,
				new List<object> { MothershipRecord.ItemType } );

			List<MothershipRecord> ships = await query.GetRemainingAsync();
			foreach( MothershipRecord ship in ships ) {
				IEnumerable<Id<MothershipModule>> moduleIds = await GetAttachedModuleIds( new Id<Mothership>( ship.MothershipId ) );
				foreach( Id<MothershipModule> moduleId in moduleIds ) {
					await DeleteModule( new Id<Mothership>( ship.MothershipId ), moduleId );
				}
				await _context.DeleteAsync<MothershipRecord>( GameRecord.GetKey( gameId.Value ), MothershipRecord.GetKey( ship.MothershipId ) );
			}
		}

		async Task<MothershipAttachedModule> IMothershipRepository.CreateModule(
			Id<Mothership> mothershipId,
			Id<MothershipModule> mothershipModuleId,
			int remainingPower,
			DateTime createdOn
		) {
			var record = new MothershipAttachedModuleRecord {
				MothershipId = mothershipId.Value,
				MothershipModuleId = mothershipModuleId.Value,
				RemainingPower = remainingPower,
				CreatedOn = createdOn.ToUniversalTime()
			};

			await _context.SaveAsync( record );

			return ToMothershipAttachedModule( record );
		}

		async Task<Mothership> IMothershipRepository.GetMothership( Id<Game> gameId ) {
			AsyncSearch<MothershipRecord> query = _context.QueryAsync<MothershipRecord>(
				GameRecord.GetKey( gameId.Value ),
				QueryOperator.BeginsWith,
				new List<object>() {
					MothershipRecord.ItemType
				} );

			List<MothershipRecord> records = await query.GetRemainingAsync();
			return ToMothership( records.First() );
		}

		private async Task<IEnumerable<Id<MothershipModule>>> GetAttachedModuleIds( Id<Mothership> mothershipId ) {
			AsyncSearch<MothershipAttachedModuleRecord> query = _context.QueryAsync<MothershipAttachedModuleRecord>(
				MothershipRecord.GetKey( mothershipId.Value ),
				QueryOperator.BeginsWith,
				new List<object> { MothershipAttachedModuleRecord.ItemType } );

			List<MothershipAttachedModuleRecord> modules = await query.GetRemainingAsync();
			return modules.Select( m => new Id<MothershipModule>( m.MothershipModuleId ) );
		}

		private async Task DeleteModule(
			Id<Mothership> mothershipId,
			Id<MothershipModule> mothershipModuleId
		) {
			var record = new MothershipAttachedModuleRecord {
				MothershipId = mothershipId.Value,
				MothershipModuleId = mothershipModuleId.Value
			};

			await _context.DeleteAsync( record );
		}

		private static Mothership ToMothership( MothershipRecord r ) {
			return new Mothership(
				new Id<Mothership>( r.MothershipId ),
				new Id<Game>( r.GameId ),
				r.Name,
				r.AvailableCrew,
				r.RemainingFuel );
		}

		private static MothershipAttachedModule ToMothershipAttachedModule( MothershipAttachedModuleRecord r ) {
			return new MothershipAttachedModule(
				new Id<Mothership>( r.MothershipId ),
				new Id<MothershipModule>( r.MothershipModuleId ),
				r.RemainingPower );
		}
	}
}
