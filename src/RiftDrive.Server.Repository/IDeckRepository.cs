﻿/*
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
using RiftDrive.Server.Model;
using RiftDrive.Shared;

namespace RiftDrive.Server.Repository {
	public interface IDeckRepository {
		Task<Deck> Create( Id<Deck> deckId, string name );

		Task AddToDeck( Id<Deck> deckId, Id<EncounterCard> cardId );

		Task RemoveFromDeck( Id<Deck> deckId, Id<EncounterCard> cardId );

		Task Delete( Id<Deck> deckId );
	}
}