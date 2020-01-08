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

		private readonly Stack<SkillCard> _draw;
		private readonly Stack<SkillCard> _discard;

		public SkillDeck( Id<SkillDeck> id ):
			this(id, new List<SkillCard>() ) {
		}

		internal SkillDeck(
			Id<SkillDeck> id,
			IEnumerable<SkillCard> cards
		) {
			Id = id;
			Cards = cards;
			_draw = new Stack<SkillCard>( cards );
			_discard = new Stack<SkillCard>();
		}

		public Id<SkillDeck> Id { get; }

		public IEnumerable<SkillCard> Cards { get; }

		public IEnumerable<SkillCard> Draw( int count ) {
			var result = new List<SkillCard>();

			for( int i = 0; i < count; i++ ) {
				_discard.Push( _draw.Peek() );
				result.Add( _draw.Pop() );
			}

			return result;
		}

		public int CheckFocus( Focus focus, int drawCount ) {
			int result = 0;
			IEnumerable<SkillCard> cards = Draw( drawCount );
			foreach ( SkillCard card in cards) {
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
