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

namespace RiftDrive.Shared.Model {
	public sealed class Mission: IEquatable<Mission> {

		public Mission(
			Id<Game> gameId,
			Id<Mission> missionId
		) {
			Id = missionId;
			GameId = gameId;
		}

		public Id<Mission> Id { get; }

		public Id<Game> GameId { get; }

		public bool Equals( Mission other ) {
			if (other is null) {
				return false;
			}

			if (ReferenceEquals(other, this)) {
				return true;
			}

			return Id.Equals( other.Id )
				&& GameId.Equals( other.GameId );
		}

		public override bool Equals( object obj ) {
			return Equals( obj as Mission );
		}

		public override int GetHashCode() {
			unchecked {
				int result = Id.GetHashCode();
				result = ( result * 31 ) + GameId.GetHashCode();

				return result;
			}
		}
	}
}
