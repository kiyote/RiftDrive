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

namespace RiftDrive.Shared.Model {
	public sealed class SkillDeck {

		private readonly Stack<SkillCard> _cards;
		private readonly Stack<SkillCard> _discard;

		public SkillDeck() {
			_cards = new Stack<SkillCard>();
			_discard = new Stack<SkillCard>();
		}

		public IEnumerable<SkillCard> Draw( int count ) {
			var result = new List<SkillCard>();

			for( int i = 0; i < count; i++ ) {
				_discard.Push( _cards.Peek() );
				result.Add( _cards.Pop() );
			}

			return result;
		}

		public int CheckSkill( Skill skill, int drawCount ) {
			int result = 0;
			IEnumerable<SkillCard> cards = Draw( drawCount );
			foreach ( SkillCard card in cards) {
				foreach (SkillValue skillValue in card.SkillValues) {
					if( skillValue.Skill == skill ) {
						result += skillValue.Value;
					}

				}
			}

			return result;
		}
	}
}
