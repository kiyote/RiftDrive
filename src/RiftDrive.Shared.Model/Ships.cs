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
using System.Collections.Generic;
using System.Linq;

namespace RiftDrive.Shared.Model {
	public sealed partial class Ship {

		public static Ship GetById( Id<Ship> shipId ) {
			return All.First( s => s.Id == shipId );
		}

		public static Ship FlaxianScout = new Ship(
			new Id<Ship>( "a3563ca08da4484a9b73110a53c86c96" ),
			"Scout",
			new List<ShipAttachedModule>() {
				new ShipAttachedModule(
					new Id<Ship>("a3563ca08da4484a9b73110a53c86c96"),
					new Id<ShipModule>("805cdfdf9f46495c8a03d8a16887eba9"), // Bridge
					0, 0),
				new ShipAttachedModule(
					new Id<Ship>("a3563ca08da4484a9b73110a53c86c96"),
					new Id<ShipModule>("112255bf754545b085edaeb4ab5e84b1"), // Engine
					0, 3)
			});

		public static Ship FlaxianFrigate = new Ship(
			new Id<Ship>( "e6bdcdc7f91e4d29869058e17b840ffc" ),
			"Frigate",
			new List<ShipAttachedModule>() {
				new ShipAttachedModule(
					new Id<Ship>("e6bdcdc7f91e4d29869058e17b840ffc"),
					new Id<ShipModule>("805cdfdf9f46495c8a03d8a16887eba9"), // Bridge
					0, 0),
				new ShipAttachedModule(
					new Id<Ship>("e6bdcdc7f91e4d29869058e17b840ffc"),
					new Id<ShipModule>("112255bf754545b085edaeb4ab5e84b1"), // Engine
					0, 3)
			} );

		public static Ship FlaxianDestroyer = new Ship(
			new Id<Ship>( "ebb20b5471b143eba824cfff6c569813" ),
			"Frigate",
			new List<ShipAttachedModule>() {
				new ShipAttachedModule(
					new Id<Ship>("ebb20b5471b143eba824cfff6c569813"),
					new Id<ShipModule>("805cdfdf9f46495c8a03d8a16887eba9"), // Bridge
					0, 0),
				new ShipAttachedModule(
					new Id<Ship>("ebb20b5471b143eba824cfff6c569813"),
					new Id<ShipModule>("112255bf754545b085edaeb4ab5e84b1"), // Engine
					0, 3)
			} );

		public static List<Ship> All = new List<Ship>() {
			FlaxianScout,
			FlaxianFrigate,
			FlaxianDestroyer,
		};
	}
}
