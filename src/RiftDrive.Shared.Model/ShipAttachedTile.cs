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
	public sealed class ShipAttachedTile : IEquatable<ShipAttachedTile> {

		public ShipAttachedTile(
			Id<Ship> shipId,
			Id<ShipTile> shipTileId
		) {
			ShipId = shipId;
			ShipTileId = shipTileId;
		}

		public Id<Ship> ShipId { get; }

		public Id<ShipTile> ShipTileId { get; }

		public bool Equals( ShipAttachedTile other ) {
			if( ReferenceEquals( other, this ) ) {
				return true;
			}

			return ShipId.Equals( other.ShipId )
				&& ShipTileId.Equals( other.ShipTileId );
		}

		public override bool Equals( object obj ) {
			if( obj is null ) {
				return false;
			}

			return Equals( obj as ShipAttachedTile );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + ShipId.GetHashCode();
				result = ( result * 31 ) + ShipTileId.GetHashCode();

				return result;
			}
		}
	}
}
