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
using Newtonsoft.Json;

namespace RiftDrive.Shared.Model {
	public sealed partial class SkillCard: IEquatable<SkillCard> {

		[JsonConstructor]
		public SkillCard(
			Id<SkillCard> id,
			IEnumerable<FocusValue> focusValues
		) {
			Id = id;
			FocusValues = focusValues;
		}

		public SkillCard(
			Id<SkillCard> id,
			FocusValue focusValue
		) {
			Id = id;
			FocusValues = new List<FocusValue>() { focusValue };
		}

		public Id<SkillCard> Id { get; set; }

		public IEnumerable<FocusValue> FocusValues { get; set; }

		public bool Equals( SkillCard? other ) {
			if( other is null ) {
				return false;
			}

			if( ReferenceEquals(other, this)) {
				return true;
			}

			return Id.Equals( other.Id )
				&& FocusValues.Similar( other.FocusValues );
		}

		public override bool Equals( object? obj ) {
			if( !( obj is SkillCard target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			return HashCode.Combine( Id, FocusValues );
		}
	}
}
