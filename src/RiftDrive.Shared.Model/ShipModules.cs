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
using System.Linq;

namespace RiftDrive.Shared.Model {
	public sealed partial class ShipModule {

		public static ShipModule GetById( Id<ShipModule> shipModuleId ) {
			return All.First( m => m.Id == shipModuleId );
		}

		public static ShipModule Bridge = new ShipModule(
			new Id<ShipModule>( "805cdfdf9f46495c8a03d8a16887eba9" ),
			"Bridge",
			new bool[3, 1] {
				{ true },
				{ true },
				{ true }
			} );

		public static ShipModule Engine = new ShipModule(
			new Id<ShipModule>( "112255bf754545b085edaeb4ab5e84b1" ),
			"Engine",
			new bool[3, 3] {
				{ false, true, false },
				{ true, true, true },
				{ false, true, false }
			} );

		public static ShipModule Cargo = new ShipModule(
			new Id<ShipModule>( "b5a372a758554448ac43d919a3f3be83" ),
			"Cargo",
			new bool[2, 2] {
				{ true, true },
				{ true, true }
			} );

		public static List<ShipModule> All = new List<ShipModule>() {
			Bridge,
			Engine,
			Cargo
		};
	}
}
