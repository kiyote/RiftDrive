﻿/*
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
using RiftDrive.Shared;

namespace RiftDrive.Server.Model {
	public sealed partial class MothershipModule {
		public static List<MothershipModule> All = new List<MothershipModule> {
			Hanger
		};

		public static MothershipModule AtmosphereProcessor = new MothershipModule(
			new Id<MothershipModule>( "21351bbe9814419d9d3c45d85242a029" ),
			"Atmosphere Processor",
			"Atmosphere Processor",
			new List<MothershipModuleAction>() {
			},
			new List<MothershipModuleEffect>() {
				new MothershipModuleEffect( ModuleEffect.ConsumePower, 1 )
			} );


		public static MothershipModule Hanger = new MothershipModule(
			new Id<MothershipModule>( "3f27c324bd954c49aab11d38fc5fd1c0" ),
			"Hanger",
			"Hanger",
			new List<MothershipModuleAction>() {
			},
			new List<MothershipModuleEffect>() {
				new MothershipModuleEffect( ModuleEffect.ConsumePower, 1 )
			} );


		public static MothershipModule Cryogenics = new MothershipModule(
			new Id<MothershipModule>( "fe9e336e4620488186726d57bfa0ba24" ),
			"Cryogenics",
			"Cryogenics",
			new List<MothershipModuleAction>() {
				new MothershipModuleAction(
					"Revive Crew",
					"Find functional pods and attempt to revive the crew.",
					new List<MothershipModuleEffect>() {
						new MothershipModuleEffect( ModuleEffect.ConsumePower, 3 ),
						new MothershipModuleEffect( ModuleEffect.AddCrew, 0, 2 )
					})
			},
			new List<MothershipModuleEffect>() {
				new MothershipModuleEffect( ModuleEffect.ConsumePower, 1 )
			} );


		public static MothershipModule Reactor = new MothershipModule(
			new Id<MothershipModule>( "a5d9728d326949b6a85a0e545cb9ba20" ),
			"Reactor",
			"Reactor",
			new List<MothershipModuleAction>() {
				new MothershipModuleAction(
					"Produce Power",
					"Run the reactor to produce power.",
					new List<MothershipModuleEffect>() {
						new MothershipModuleEffect( ModuleEffect.ProducePower, 5 ),
						new MothershipModuleEffect( ModuleEffect.ConsumeFuel, 10 )
					}),

			},
			new List<MothershipModuleEffect>() {

			} );
	}
}
