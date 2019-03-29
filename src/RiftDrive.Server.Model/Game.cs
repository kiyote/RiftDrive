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
using System.Text;
using RiftDrive.Shared;

namespace RiftDrive.Server.Model {
	public sealed class Game: IEquatable<Game> {

		public Game(
			Id<Game> gameId,
			string name,
			DateTime createdOn,
			GameState state
		) {
			Id = gameId;
			Name = name;
			CreatedOn = createdOn.ToUniversalTime();
			State = state;
		}

		public Id<Game> Id { get; }

		public string Name { get; }

		public DateTime CreatedOn { get; }

		public GameState State { get; }

		public bool Equals( Game other ) {
			if (ReferenceEquals(other, null)) {
				return false;
			}

			if (ReferenceEquals(other, this)) {
				return true;
			}

			return Id.Equals( other.Id )
				&& string.Equals( Name, other.Name, StringComparison.Ordinal )
				&& DateTime.Equals( CreatedOn, other.CreatedOn )
				&& State.Equals( other.State );
		}

		public override bool Equals( object obj ) {
			return Equals( obj as Game );
		}

		public override int GetHashCode() {
			unchecked {
				var result = 17;
				result = ( 31 * result ) + Id.GetHashCode();
				result = ( 31 * result ) + Name.GetHashCode();
				result = ( 31 * result ) + CreatedOn.GetHashCode();
				result = ( 31 * result ) + State.GetHashCode();

				return result;
			}
		}
	}
}
