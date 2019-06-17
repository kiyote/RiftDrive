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
	public sealed class SkillCheckOutcomes : IEquatable<SkillCheckOutcomes> {

		public static SkillCheckOutcomes None = new SkillCheckOutcomes( Skill.Unknown, int.MinValue, int.MinValue, int.MinValue );

		[JsonConstructor]
		public SkillCheckOutcomes(
			Skill skill,
			int target,
			int success,
			int failure
		) {
			Skill = skill;
			Target = target;
			Success = success;
			Failure = failure;
		}

		public Skill Skill { get; }

		public int Target { get; }

		public int Success { get; }

		public int Failure { get; }

		public bool Equals( SkillCheckOutcomes other ) {
			if( ReferenceEquals( other, this ) ) {
				return true;
			}

			return Skill == other.Skill
				&& Target == other.Target
				&& Success == other.Success
				&& Failure == other.Failure;
		}

		public override bool Equals( object obj ) {
			if( !( obj is SkillCheckOutcomes target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + Skill.GetHashCode();
				result = ( result * 31 ) + Target.GetHashCode();
				result = ( result * 31 ) + Success.GetHashCode();
				result = ( result * 31 ) + Failure.GetHashCode();

				return result;
			}
		}

		public string ToDisplay(bool includeParentheses = true) {
			string result = "-";

			if (this.Skill != Skill.None) {
				result = $"{this.Skill} {this.Target}";
			}

			if (includeParentheses) {
				return $"({result})";
			} else {
				return result;
			}
		}
	}
}
