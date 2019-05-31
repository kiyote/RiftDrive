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
using RiftDrive.Shared.Model;

namespace RiftDrive.Server.Model {
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

		public bool Equals( EncounterOutcomeCard other ) {
			if (ReferenceEquals( other, this )) {
				return true;
			}

			return Id.Equals( other.Id )
				&& Outcomes.Similar( other.Outcomes );
		}

		public override bool Equals( object obj ) {
			if( !( obj is EncounterOutcomeCard target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + Id.GetHashCode();
				result = ( result * 31 ) + Outcomes.GetFinalHashCode();

				return result;
			}
		}
	}
}
