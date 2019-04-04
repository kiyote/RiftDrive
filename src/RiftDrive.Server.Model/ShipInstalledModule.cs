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
	public sealed class ShipInstalledModule: IEquatable<ShipInstalledModule> {

		public ShipInstalledModule(
			Id<Ship> shipId,
			Id<ShipModule> moduleId
		) {
			ShipId = shipId;
			ModuleId = moduleId;
		}

		public Id<Ship> ShipId { get; }

		public Id<ShipModule> ModuleId { get; }

		public bool Equals( ShipInstalledModule other ) {
			if (other is null) {
				return false;
			}

			if (ReferenceEquals(other, this)) {
				return true;
			}

			return ShipId.Equals( other.ShipId )
				&& ModuleId.Equals( other.ModuleId );
		}

		public override bool Equals( object obj ) {
			return Equals( obj as ShipInstalledModule );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + ShipId.GetHashCode();
				result = ( result * 31 ) + ModuleId.GetHashCode();

				return result;
			}
		}
	}
}
