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
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using RiftDrive.Server.Model;
using RiftDrive.Server.Repository.DynamoDb.Model;
using RiftDrive.Shared.Model;

namespace RiftDrive.Server.Repository.DynamoDb {
	internal sealed class RaceRepository : IRaceRepository {

		private readonly IDynamoDBContext _context;

		public RaceRepository(
			IDynamoDBContext context
		) {
			_context = context;
		}

		async Task<Race> IRaceRepository.Get(
			Id<Race> raceId
		) {
			RaceRecord raceRecord = await _context.LoadAsync<RaceRecord>( RaceRecord.GetKey( raceId.Value ), RaceRecord.GetKey( raceId.Value ) );

			return ToRace( raceRecord );
		}

		private static Race ToRace( RaceRecord r ) {
			return new Race(
				new Id<Race>( r.RaceId ),
				r.Name );
		}
	}
}
