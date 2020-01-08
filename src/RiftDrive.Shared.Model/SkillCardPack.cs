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
	public sealed partial class SkillCardPack: IEquatable<SkillCardPack> {

		public SkillCardPack(
			Id<SkillCardPack> id,
			IEnumerable<Id<SkillCard>> cards
		) {
			Id = id;
			Cards = cards;
		}

		public Id<SkillCardPack> Id { get; }

		public IEnumerable<Id<SkillCard>> Cards { get; }

		public bool Equals( SkillCardPack other ) {
			if (ReferenceEquals( other, this)) {
				return true;
			}

			return Id.Equals( other.Id )
				&& Cards.Similar( other.Cards );
		}

		public override bool Equals( object obj ) {
			if( !( obj is SkillCardPack target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + Id.GetHashCode();
				result = ( result * 31 ) + Cards.GetFinalHashCode();

				return result;
			}
		}
	}
}
