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
using Newtonsoft.Json;

namespace RiftDrive.Shared.Model {
	public sealed partial class SkillCard: IEquatable<SkillCard> {

		[JsonConstructor]
		public SkillCard(
			Id<SkillCard> id,
			IEnumerable<SkillValue> skillValues
		) {
			Id = id;
			SkillValues = skillValues;
		}

		public SkillCard(
			Id<SkillCard> id,
			SkillValue skillValue
		) {
			Id = id;
			SkillValues = new List<SkillValue>() { skillValue };
		}

		public Id<SkillCard> Id { get; set; }

		public IEnumerable<SkillValue> SkillValues { get; set; }

		public bool Equals( SkillCard other ) {
			if (ReferenceEquals(other, this)) {
				return true;
			}

			return Id.Equals( other.Id )
				&& SkillValues.Similar( other.SkillValues );
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
				result = ( result * 31 ) + SkillValues.GetFinalHashCode();

				return result;
			}
		}
	}
}
