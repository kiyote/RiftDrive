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
using Newtonsoft.Json;

namespace RiftDrive.Shared.Model {
	public sealed class EncounterInteraction: IEquatable<EncounterInteraction> {

		[JsonConstructor]
		public EncounterInteraction(
			Id<EncounterInteraction> id,
			string description,
			EncounterInteractionCheck outcomes
		) {
			Id = id;
			Description = description;
			Outcomes = outcomes;
		}

		public Id<EncounterInteraction> Id { get; }

		public string Description { get; }

		public EncounterInteractionCheck Outcomes { get; }

		bool IEquatable<EncounterInteraction>.Equals( EncounterInteraction other ) {
			if (ReferenceEquals(other, this)) {
				return true;
			}

			return Id.Equals( other.Id )
				&& string.Equals( Description, other.Description, StringComparison.Ordinal )
				&& Equals( Outcomes, other.Outcomes );
		}

		public override bool Equals( object obj ) {
			if( !( obj is EncounterInteraction target ) ) {
				return false;
			}

			return Equals( target as EncounterInteraction );
		}

		public override int GetHashCode() {
			return HashCode.Combine( Id, Description, Outcomes );
		}
	}
}
