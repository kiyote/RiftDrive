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

namespace RiftDrive.Shared.Model.Client {
	public class ClientPlayer : IEquatable<ClientPlayer> {

		[JsonConstructor]
		public ClientPlayer(
			Id<ClientPlayer> id,
			Id<Game> gameId,
			string name
		) {
			Id = id;
			GameId = gameId;
			Name = name;
		}

		public Id<ClientPlayer> Id { get; }

		public Id<Game> GameId { get; }

		public string Name { get; }

		public bool Equals( ClientPlayer other ) {
			if( other is null ) {
				return false;
			}

			if( ReferenceEquals( other, this ) ) {
				return true;
			}

			return Id.Equals( other.Id )
				&& GameId.Equals( other.GameId )
				&& string.Equals( Name, other.Name, StringComparison.Ordinal );
		}

		public override bool Equals( object obj ) {
			if( !( obj is ClientPlayer target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			return HashCode.Combine( Id, GameId, Name );
		}
	}
}
