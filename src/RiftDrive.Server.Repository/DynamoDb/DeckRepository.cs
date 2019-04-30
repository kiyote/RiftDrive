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
	internal sealed class DeckRepository : IDeckRepository {

		private readonly IDynamoDBContext _context;

		public DeckRepository(
			IDynamoDBContext context
		) {
			_context = context;
		}

		async Task<Deck> IDeckRepository.Create(
			Id<Deck> deckId,
			string name
		) {
			var record = new DeckRecord {
				DeckId = deckId.Value,
				Name = name
			};
			await _context.SaveAsync( record );

			return ToDeck( record );
		}

		async Task IDeckRepository.AddToDeck(
			Id<Deck> deckId,
			Id<EncounterCard> cardId
		) {
			var record = new EncounterDeckCardRecord {
				DeckId = deckId.Value,
				EncounterCardId = cardId.Value
			};

			await _context.SaveAsync( record );
		}

		async Task IDeckRepository.RemoveFromDeck(
			Id<Deck> deckId,
			Id<EncounterCard> cardId
		) {
			var record = new EncounterDeckCardRecord {
				DeckId = deckId.Value,
				EncounterCardId = cardId.Value
			};

			await _context.DeleteAsync( record );
		}

		async Task IDeckRepository.Delete( Id<Deck> deckId ) {
			var record = new DeckRecord {
				DeckId = deckId.Value
			};

			await _context.DeleteAsync( record );
		}

		private static Deck ToDeck( DeckRecord r ) {
			return new Deck(
				new Id<Deck>( r.DeckId ),
				r.Name );
		}
	}
}
