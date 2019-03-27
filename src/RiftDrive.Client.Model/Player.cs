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
using RiftDrive.Shared;
using Newtonsoft.Json;

namespace RiftDrive.Client.Model {
	public class Player : IEquatable<Player> {

		[JsonConstructor]
		public Player(
			Id<Player> id,
			Id<Game> gameId,
			string name
		) {
			Id = id;
			GameId = gameId;
			Name = name;
		}

		public Id<Player> Id { get; }

		public Id<Game> GameId { get; }

		public string Name { get; }

		public bool Equals( Player other ) {
			if( ReferenceEquals( other, null ) ) {
				return false;
			}

			if( ReferenceEquals( other, this ) ) {
				return true;
			}

			return Id.Equals( other.Id )
				&& GameId.Equals(other.GameId)
				&& string.Equals( Name, other.Name, StringComparison.Ordinal );
		}

		public override bool Equals( object obj ) {
			return Equals( obj as Player );
		}

		public override int GetHashCode() {
			unchecked {
				var result = 17;
				result = ( result * 31 ) + Id.GetHashCode();
				result = ( result * 31 ) + GameId.GetHashCode();
				result = ( result * 13 ) + Name.GetHashCode();

				return result;
			}
		}
	}
}
