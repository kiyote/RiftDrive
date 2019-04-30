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
using RiftDrive.Shared;

namespace RiftDrive.Shared.Model {
	public sealed class Actor: IEquatable<Actor> {

		[JsonConstructor]
		public Actor(
			Id<Actor> id,
			Id<Game> gameId,
			string name,
			Role role,
			int intelligence,
			int talent,
			int training
		) {
			Id = id;
			GameId = gameId;
			Name = name;
			Role = role;
			Intelligence = intelligence;
			Talent = talent;
			Training = training;
		}

		public Id<Actor> Id { get; }

		public Id<Game> GameId { get; }

		public string Name { get; }

		public Role Role { get; }

		/// <summary>
		/// The number of cards you can draw.
		/// </summary>
		public int Intelligence { get; }

		/// <summary>
		/// The number of cards you can hold.
		/// </summary>
		public int Talent { get; }

		/// <summary>
		/// The number of cards you can play.
		/// </summary>
		public int Training { get; }

		public bool Equals( Actor other ) {
			if (ReferenceEquals(other, this)) {
				return true;
			}

			return Id.Equals( other.Id )
				&& GameId.Equals( other.GameId )
				&& string.Equals( Name, other.Name, StringComparison.Ordinal )
				&& Role == other.Role
				&& Intelligence == other.Intelligence
				&& Talent == other.Talent
				&& Training == other.Training;
		}

		public override bool Equals( object obj ) {
			if( !( obj is Actor target ) ) {
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
				result = ( result * 31 ) + Role.GetHashCode();
				result = ( result * 31 ) + Intelligence.GetHashCode();
				result = ( result * 31 ) + Talent.GetHashCode();
				result = ( result * 31 ) + Training.GetHashCode();

				return result;
			}
		}
	}
}
