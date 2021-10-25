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
using System.Linq;

namespace RiftDrive.Shared.Model {
	public sealed partial class EncounterOutcomeCard : IEquatable<EncounterOutcomeCard> {

		public EncounterOutcomeCard(
			Id<EncounterOutcomeCard> id,
			IEnumerable<EncounterOutcome> outcomes
		) {
			Id = id;
			Outcomes = outcomes;
		}

		public Id<EncounterOutcomeCard> Id { get; }

		public IEnumerable<EncounterOutcome> Outcomes { get; }

		public EncounterOutcome GetResult(int magnitude) {
			return Outcomes.Last( o => o.Low <= magnitude && o.High >= magnitude );
		}

		public bool Equals( EncounterOutcomeCard? other ) {
			if (other is null) {
				return false;
			}

			if (ReferenceEquals( other, this )) {
				return true;
			}

			return Id.Equals( other.Id )
				&& Outcomes.Similar( other.Outcomes );
		}

		public override bool Equals( object? obj ) {
			if( !( obj is EncounterOutcomeCard target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			return HashCode.Combine( Id, Outcomes );
		}
	}
}
