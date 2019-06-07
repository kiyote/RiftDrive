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
using Newtonsoft.Json;

namespace RiftDrive.Shared.Model {
	public sealed class SkillCheck : IEquatable<SkillCheck> {

		public static SkillCheck None = new SkillCheck( Skill.Unknown, int.MinValue );

		[JsonConstructor]
		public SkillCheck(
			Skill skill,
			int target
		) {
			Skill = skill;
			Target = target;
		}

		public Skill Skill { get; }

		public int Target { get; }

		public bool Equals( SkillCheck other ) {
			if( ReferenceEquals( other, this ) ) {
				return true;
			}

			return Skill == other.Skill
				&& Target == other.Target;
		}

		public override bool Equals( object obj ) {
			if( !( obj is SkillCheck target ) ) {
				return false;
			}

			return Equals( target as SkillCheck );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + Skill.GetHashCode();
				result = ( result * 31 ) + Target.GetHashCode();

				return result;
			}
		}
	}
}
