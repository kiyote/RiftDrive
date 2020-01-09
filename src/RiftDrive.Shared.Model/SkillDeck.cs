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

namespace RiftDrive.Shared.Model {
	public sealed partial class SkillDeck {

		private readonly Stack<SkillDeckCard> _draw;
		private readonly Stack<SkillDeckCard> _discard;

		public SkillDeck( Id<Actor> actorId ) :
			this( actorId, new List<SkillDeckCard>() ) {
		}

		public SkillDeck(
			Id<Actor> actorId,
			IEnumerable<SkillDeckCard> cards
		) {
			ActorId = actorId;
			_draw = new Stack<SkillDeckCard>( cards );
			_discard = new Stack<SkillDeckCard>();
		}

		public SkillDeck(
			Id<Actor> actorId,
			IEnumerable<SkillDeckCard> drawPile,
			IEnumerable<SkillDeckCard> discardPile
		) {
			ActorId = actorId;
			_draw = new Stack<SkillDeckCard>( drawPile );
			_discard = new Stack<SkillDeckCard>( discardPile );
		}

		public Id<Actor> ActorId { get; }

		public IEnumerable<SkillDeckCard> Draw( int count ) {
			var result = new List<SkillDeckCard>();

			for( int i = 0; i < count; i++ ) {
				_discard.Push( _draw.Peek() );
				result.Add( _draw.Pop() );
			}

			return result;
		}

		public int CheckFocus( Focus focus, int drawCount ) {
			int result = 0;
			IEnumerable<SkillDeckCard> cards = Draw( drawCount );
			foreach ( SkillDeckCard deckCard in cards) {
				SkillCard card = SkillCard.GetById( deckCard.SkillCardId );
				foreach (FocusValue focusValue in card.FocusValues) {
					if( focusValue.Focus == focus ) {
						result += focusValue.Value;
					}

				}
			}

			return result;
		}
	}
}
