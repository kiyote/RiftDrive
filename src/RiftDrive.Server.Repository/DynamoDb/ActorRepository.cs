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
			IEnumerable<Skill> skills,
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

			foreach ( Skill skill in skills) {
				var actorSkillRecord = new ActorSkillRecord {
					GameId = gameId.Value,
					ActorId = actorId.Value,
					SkillId = skill.Id.Value
				};
				await _context.SaveAsync( actorSkillRecord );
			}

			return ToActor( actorRecord, skills );
		}

		async Task<IEnumerable<Actor>> IActorRepository.GetActors( Id<Game> gameId ) {
			AsyncSearch<ActorRecord> query = _context.QueryAsync<ActorRecord>( GameRecord.GetKey( gameId.Value ),
				QueryOperator.BeginsWith,
				new List<object>() { ActorRecord.ItemType } );

			List<ActorRecord> actors = await query.GetRemainingAsync();

			IActorRepository repository = this;
			return await Task.WhenAll(actors.Select( async( a ) => {
				IEnumerable<Skill> skills = await repository.GetActorSkills( gameId, new Id<Actor>( a.ActorId ) );
				return ToActor( a, skills );
			} ));
		}

		async Task<Actor?> IActorRepository.GetActor( Id<Game> gameId, Id<Actor> actorId ) {
			ActorRecord actorRecord = await _context.LoadAsync<ActorRecord>( GameRecord.GetKey( gameId.Value ), ActorRecord.GetKey( actorId.Value ) );

			if (actorRecord == default) {
				return default;
			}

			IActorRepository repository = this;
			IEnumerable<Skill> skills = await repository.GetActorSkills( gameId, actorId );
			return ToActor( actorRecord, skills );
		}

		async Task IActorRepository.Delete( Id<Game> gameId, Id<Actor> actorId ) {
			await _context.DeleteAsync<ActorRecord>( GameRecord.GetKey( gameId.Value ), ActorRecord.GetKey( actorId.Value ) );

			AsyncSearch<ActorSkillRecord> query = _context.QueryAsync<ActorSkillRecord>(
				ActorRecord.GetKey( actorId.Value ),
				QueryOperator.BeginsWith,
				new List<object>() {
					ActorSkillRecord.ItemType
				} );

			List<ActorSkillRecord> records = await query.GetRemainingAsync();
			foreach( ActorSkillRecord r in records ) {
				await _context.DeleteAsync( r );
			}
		}

		async Task<IEnumerable<Skill>> IActorRepository.GetActorSkills( Id<Game> gameId, Id<Actor> actorId ) {
			AsyncSearch<ActorSkillRecord> query = _context.QueryAsync<ActorSkillRecord>(
				ActorRecord.GetKey( actorId.Value ),
				QueryOperator.BeginsWith,
				new List<object>() {
					ActorSkillRecord.ItemType
				} );

			List<ActorSkillRecord> records = await query.GetRemainingAsync();

			return records.Select( r => Skill.GetById( new Id<Skill>( r.SkillId ) ) );
		}

		async Task<SkillDeck> IActorRepository.GetSkillDeck( Id<Mission> missionId, Id<Actor> actorId ) {
			AsyncSearch<ActorSkillDeckRecord> query = _context.QueryAsync<ActorSkillDeckRecord>(
				ActorRecord.GetKey( actorId.Value ),
				QueryOperator.BeginsWith,
				new List<object>() {
					ActorSkillDeckRecord.ItemType
				} );

			List<ActorSkillDeckRecord> records = await query.GetRemainingAsync();

			return new SkillDeck(
				actorId,
				records
					.Where( r => string.Equals( r.CardPile, DeckPile.Draw.ToString(), StringComparison.OrdinalIgnoreCase ) )
					.Select( r => new SkillDeckCard( new Id<SkillCard>( r.SkillCardId ), new Id<SkillDeckCard>( r.InstanceId ) ) ),
				records
					.Where( r => string.Equals( r.CardPile, DeckPile.Discard.ToString(), StringComparison.OrdinalIgnoreCase ) )
					.Select( r => new SkillDeckCard( new Id<SkillCard>( r.SkillCardId ), new Id<SkillDeckCard>( r.InstanceId ) ) )
				);
		}

		async Task<SkillDeck> IActorRepository.CreateSkillDeck( Id<Mission> missionId, Id<Actor> actorId, IEnumerable<SkillDeckCard> skillCards ) {
			foreach( SkillDeckCard card in skillCards ) {
				var actorSkillCardRecord = new ActorSkillDeckRecord {
					ActorId = actorId.Value,
					SkillCardId = card.SkillCardId.Value,
					InstanceId = card.InstanceId.Value,
					MissionId = missionId.Value,
					CardPile = DeckPile.Draw.ToString()
				};
				await _context.SaveAsync( actorSkillCardRecord );
			}

			return new SkillDeck( actorId, skillCards );
		}

		async Task<SkillDeck> IActorRepository.UpdateSkillDeck( Id<Mission> missionId, Id<Actor> actorId, IEnumerable<SkillDeckCard> drawCards, IEnumerable<SkillDeckCard> discardCards ) {
			foreach( SkillDeckCard card in drawCards ) {
				var actorSkillCardRecord = new ActorSkillDeckRecord {
					ActorId = actorId.Value,
					SkillCardId = card.SkillCardId.Value,
					InstanceId = card.InstanceId.Value,
					MissionId = missionId.Value,
					CardPile = DeckPile.Draw.ToString()
				};
				await _context.SaveAsync( actorSkillCardRecord );
			}

			foreach( SkillDeckCard card in discardCards ) {
				var actorSkillCardRecord = new ActorSkillDeckRecord {
					ActorId = actorId.Value,
					SkillCardId = card.SkillCardId.Value,
					InstanceId = card.InstanceId.Value,
					MissionId = missionId.Value,
					CardPile = DeckPile.Discard.ToString()
				};
				await _context.SaveAsync( actorSkillCardRecord );
			}

			return new SkillDeck( actorId, drawCards, discardCards );
		}

		private static Actor ToActor(
			ActorRecord r,
			IEnumerable<Skill> skills
		) {
			return new Actor(
				new Id<Actor>( r.ActorId ),
				new Id<Game>( r.GameId ),
				r.Name,
				Enum.Parse<Role>( r.Role ),
				r.Discipline,
				r.Expertise,
				r.Training,
				skills );
		}
	}
}
