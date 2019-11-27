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
using RiftDrive.Server.Repository.DynamoDb.Model;
using RiftDrive.Shared.Model;

namespace RiftDrive.Server.Repository.DynamoDb {
	internal sealed class ActorRepository : IActorRepository {

		private readonly IDynamoDBContext _context;

		public ActorRepository(
			IDynamoDBContext context
		) {
			_context = context;
		}

		async Task<Actor> IActorRepository.Create(
			Id<Game> gameId,
			Id<Actor> actorId,
			string name,
			Role role,
			int discipline,
			int expertise,
			int training,
			DateTime createdOn
		) {
			var actorRecord = new ActorRecord {
				GameId = gameId.Value,
				ActorId = actorId.Value,
				Name = name,
				Role = role.ToString(),
				Discipline = discipline,
				Expertise = expertise,
				Training = training,
				CreatedOn = createdOn.ToUniversalTime()
			};
			await _context.SaveAsync( actorRecord );

			return ToActor( actorRecord );
		}

		async Task<IEnumerable<Actor>> IActorRepository.GetActors( Id<Game> gameId ) {
			AsyncSearch<ActorRecord> query = _context.QueryAsync<ActorRecord>( GameRecord.GetKey( gameId.Value ),
				QueryOperator.BeginsWith,
				new List<object>() { ActorRecord.ItemType } );

			List<ActorRecord> actors = await query.GetRemainingAsync();

			return actors.Select( a => ToActor( a ) );
		}

		async Task<Actor?> IActorRepository.GetActor( Id<Game> gameId, Id<Actor> actorId ) {
			ActorRecord actorRecord = await _context.LoadAsync<ActorRecord>( GameRecord.GetKey( gameId.Value ), ActorRecord.GetKey( actorId.Value ) );

			if (actorRecord == default) {
				return default;
			}

			return ToActor( actorRecord );
		}

		async Task IActorRepository.Delete( Id<Game> gameId, Id<Actor> actorId ) {
			await _context.DeleteAsync<ActorRecord>( GameRecord.GetKey( gameId.Value ), ActorRecord.GetKey( actorId.Value ) );
		}

		private static Actor ToActor( ActorRecord r ) {
			return new Actor(
				new Id<Actor>( r.ActorId ),
				new Id<Game>( r.GameId ),
				r.Name,
				(Role)Enum.Parse( typeof( Role ), r.Role ),
				r.Discipline,
				r.Expertise,
				r.Training );
		}
	}
}
