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
	public sealed partial class EncounterCard: IEquatable<EncounterCard> {

		[JsonConstructor]
		public EncounterCard(
			Id<EncounterCard> id,
			string description,
			RoleFocusCheck revealRace,
			IEnumerable<EncounterInteraction> interactions
		) {
			Id = id;
			Description = description;
			RevealRace = revealRace;
			Interactions = interactions;
		}

		public Id<EncounterCard> Id { get; }

		public string Description { get; }

		public RoleFocusCheck RevealRace { get; }

		public IEnumerable<EncounterInteraction> Interactions { get; }

		public bool Equals( EncounterCard other ) {
			if (ReferenceEquals(other, this)) {
				return true;
			}

			return Id.Equals( other.Id )
				&& string.Equals( Description, other.Description, StringComparison.Ordinal )
				&& RevealRace.Equals( other.RevealRace )
				&& Interactions.Similar( other.Interactions );
		}

		public override bool Equals( object obj ) {
			if( !( obj is EncounterCard target ) ) {
				return false;
			}

			return Equals( target as EncounterCard );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + Id.GetHashCode();
				result = ( result * 31 ) + Description.GetHashCode();
				result = ( result * 31 ) + RevealRace.GetHashCode();
				result = ( result * 31 ) + Interactions.GetFinalHashCode();

				return result;
			}
		}
	}
}
