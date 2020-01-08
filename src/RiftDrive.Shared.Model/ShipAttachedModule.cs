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

namespace RiftDrive.Shared.Model {
	public sealed class ShipAttachedModule : IEquatable<ShipAttachedModule> {

		public ShipAttachedModule(
			Id<Ship> shipId,
			Id<ShipModule> shipModuleId,
			int tileColumn,
			int tileRow
		) {
			ShipId = shipId;
			ShipModuleId = shipModuleId;
			TileRow = tileRow;
			TileColumn = tileColumn;
		}

		public Id<Ship> ShipId { get; }

		public Id<ShipModule> ShipModuleId { get; }

		public int TileRow { get; }

		public int TileColumn { get; }

		public bool Equals( ShipAttachedModule other ) {
			if( ReferenceEquals( other, this ) ) {
				return true;
			}

			return ShipId.Equals( other.ShipId )
				&& ShipModuleId.Equals( other.ShipModuleId )
				&& TileRow == other.TileRow
				&& TileColumn == other.TileColumn;
		}

		public override bool Equals( object obj ) {
			if( !( obj is ShipAttachedModule target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			return HashCode.Combine( ShipId, ShipModuleId, TileRow, TileColumn );
		}
	}
}
