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
using RiftDrive.Shared;
using Newtonsoft.Json;

namespace RiftDrive.Shared.Model {
	public class Game : IEquatable<Game> {

		[JsonConstructor]
		public Game(
			Id<Game> id,
			string name,
			DateTime createdOn,
			GameState state
		) {
			Id = id;
			Name = name;
			CreatedOn = createdOn;
			State = state;
		}

		public Id<Game> Id { get; }

		public string Name { get; }

		public DateTime CreatedOn { get; }

		public GameState State { get; }

		public bool Equals( Game other ) {
			if (other is null) {
				return false;
			}

			if( ReferenceEquals( other, this ) ) {
				return true;
			}

			return Id.Equals( other.Id )
				&& string.Equals( Name, other.Name, StringComparison.Ordinal )
				&& DateTime.Equals( CreatedOn, other.CreatedOn )
				&& State == other.State;
		}

		public override bool Equals( object obj ) {
			if( !( obj is Game target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + Id.GetHashCode();
				result = ( result * 13 ) + Name.GetHashCode();
				result = ( result * 31 ) + CreatedOn.GetHashCode();
				result = ( result * 31 ) + State.GetHashCode();

				return result;
			}
		}
	}
}
