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

namespace RiftDrive.Shared.Model {
	public sealed partial class SkillCard: IEquatable<SkillCard> {

		public SkillCard(
			Id<SkillCard> id,
			Skill skill,
			int magnitude
		) {
			Id = id;
			Skill = skill;
			Magnitude = magnitude;
		}

		public Id<SkillCard> Id { get; set; }

		public Skill Skill { get; set; }

		public int Magnitude { get; set; }

		public bool Equals( SkillCard other ) {
			if (ReferenceEquals(other, this)) {
				return true;
			}

			return Id.Equals( other.Id )
				&& Skill == other.Skill
				&& Magnitude == other.Magnitude;
		}

		public override bool Equals( object obj ) {
			SkillCard? card = obj as SkillCard;

			if (card == default) {
				return false;
			}

			return Equals( card );
		}

		public override int GetHashCode() {
			unchecked {
				int result = Id.GetHashCode();
				result = ( result * 31 ) + Skill.GetHashCode();
				result = ( result * 31 ) + Magnitude.GetHashCode();

				return result;
			}
		}
	}
}
