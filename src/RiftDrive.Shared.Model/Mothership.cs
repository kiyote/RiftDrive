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
	public sealed class Mothership : IEquatable<Mothership> {

		[JsonConstructor]
		public Mothership(
			Id<Mothership> id,
			Id<Game> gameId,
			string name,
			int availableCrew,
			int remainingFuel
		) {
			Id = id;
			Name = name;
			GameId = gameId;
			AvailableCrew = availableCrew;
			RemainingFuel = remainingFuel;
		}

		public Id<Mothership> Id { get; }

		public Id<Game> GameId { get; }

		public string Name { get; }

		public int AvailableCrew { get; }

		public int RemainingFuel { get; }

		public bool Equals( Mothership other ) {
			if( ReferenceEquals( other, this ) ) {
				return true;
			}

			return Id.Equals( other.Id )
				&& GameId.Equals( other.GameId )
				&& string.Equals( Name, other.Name, StringComparison.Ordinal )
				&& AvailableCrew == other.AvailableCrew
				&& RemainingFuel == other.RemainingFuel;
		}

		public override bool Equals( object obj ) {
			if( !( obj is Mothership target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + Id.GetHashCode();
				result = ( result * 31 ) + GameId.GetHashCode();
				result = ( result * 31 ) + Name.GetHashCode();
				result = ( result * 31 ) + AvailableCrew.GetHashCode();
				result = ( result * 31 ) + RemainingFuel.GetHashCode();

				return result;
			}
		}
	}
}
