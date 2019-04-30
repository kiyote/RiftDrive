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
using RiftDrive.Shared.Model;

namespace RiftDrive.Server.Repository.DynamoDb {
	internal sealed class EncounterCardRepository : IEncounterCardRepository {

		private readonly IDynamoDBContext _context;

		public EncounterCardRepository(
			IDynamoDBContext context
		) {
			_context = context;
		}

		async Task<EncounterCard> IEncounterCardRepository.Create(
			Id<EncounterCard> id,
			string description,
			SkillCheck skillCheck,
			IEnumerable<EncounterInteraction> interactions
		) {
			var record = new EncounterCardRecord {
				EncounterCardId = id.Value,
				Description = description,
				RevealRaceSkill = skillCheck.Skill.ToString(),
				RevealRaceTarget = skillCheck.Target
			};

			await _context.SaveAsync( record );

			var interactionRecords = new List<EncounterCardInteractionRecord>();
			foreach( var interaction in interactions ) {
				var interactionRecord = new EncounterCardInteractionRecord {
					EncounterCardId = id.Value,
					InteractionId = interaction.Id.Value,
					Description = interaction.Description,
					OutcomeSkill = interaction.Outcomes.Skill.ToString(),
					OutcomeTarget = interaction.Outcomes.Target,
					OutcomeSuccess = interaction.Outcomes.Success,
					OutcomeFailure = interaction.Outcomes.Failure
				};
				interactionRecords.Add( interactionRecord );

				await _context.SaveAsync( interactionRecord );
			}

			return ToEncounterCard( record, interactionRecords );
		}

		async Task<IEnumerable<Id<EncounterCard>>> IEncounterCardRepository.GetCardIds( Id<Deck> deckId ) {
			AsyncSearch<EncounterDeckCardRecord> cardRecords = _context.QueryAsync<EncounterDeckCardRecord>(
				DeckRecord.GetKey( deckId.Value ),
				QueryOperator.BeginsWith,
				new List<object> { EncounterCardRecord.ItemType } );

			List<EncounterDeckCardRecord> cards = await cardRecords.GetRemainingAsync();

			return cards.Select( c => new Id<EncounterCard>( c.EncounterCardId ) );
		}

		async Task<IEnumerable<EncounterCard>> IEncounterCardRepository.GetCards(
			IEnumerable<Id<EncounterCard>> cardIds
		) {
			var batchGet = _context.CreateBatchGet<EncounterCardRecord>();
			foreach( var cardId in cardIds ) {
				batchGet.AddKey( EncounterCardRecord.GetKey( cardId.Value ), EncounterCardRecord.GetKey( cardId.Value ) );
			}
			await batchGet.ExecuteAsync();

			var result = new List<EncounterCard>();
			foreach( var card in batchGet.Results ) {

				result.Add( ToEncounterCard( card, await GetInteractions( card.EncounterCardId ) ) );
			}

			return result;
		}

		async Task<EncounterCard> IEncounterCardRepository.GetCard( Id<EncounterCard> cardId ) {
			EncounterCardRecord encounterCardRecord = await _context.LoadAsync<EncounterCardRecord>( EncounterCardRecord.GetKey( cardId.Value ), EncounterCardRecord.GetKey( cardId.Value ) );
			IEnumerable<EncounterCardInteractionRecord> interactionRecords = await GetInteractions( cardId.Value );

			return ToEncounterCard( encounterCardRecord, interactionRecords );
		}

		private async Task<IEnumerable<EncounterCardInteractionRecord>> GetInteractions( string encounterCardId ) {
			AsyncSearch<EncounterCardInteractionRecord> query = _context.QueryAsync<EncounterCardInteractionRecord>(
				EncounterCardRecord.GetKey( encounterCardId ),
				QueryOperator.BeginsWith,
				new List<object> { EncounterCardInteractionRecord.ItemType } );

			return await query.GetRemainingAsync();
		}

		private static EncounterCard ToEncounterCard(
			EncounterCardRecord r,
			IEnumerable<EncounterCardInteractionRecord> ir
		) {
			var interactions = ir.Select( i => new EncounterInteraction(
				new Id<EncounterInteraction>( i.InteractionId ),
				i.Description,
				new SkillCheckOutcomes(
					(Skill)Enum.Parse( typeof( Skill ), i.OutcomeSkill ),
					i.OutcomeTarget,
					i.OutcomeSuccess,
					i.OutcomeFailure )
				) );
			return new EncounterCard(
				new Id<EncounterCard>( r.EncounterCardId ),
				r.Description,
				new SkillCheck(
					(Skill)Enum.Parse( typeof( Skill ), r.RevealRaceSkill ),
					r.RevealRaceTarget ),
				interactions
				);
		}
	}
}
