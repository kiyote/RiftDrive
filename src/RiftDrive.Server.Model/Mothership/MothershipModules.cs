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

namespace RiftDrive.Server.Model.Mothership {
	public sealed partial class MothershipModule {
		public static List<MothershipModule> All = new List<MothershipModule> {
			Hanger
		};

		public static MothershipModule AtmosphereProcessor = new MothershipModule( new Id<MothershipModule>( "21351bbe9814419d9d3c45d85242a029" ), "Atmosphere Processor", "Atmosphere Processor" );
		public static MothershipModule Hanger = new MothershipModule( new Id<MothershipModule>( "3f27c324bd954c49aab11d38fc5fd1c0" ), "Hanger", "Hanger" );
	}
}