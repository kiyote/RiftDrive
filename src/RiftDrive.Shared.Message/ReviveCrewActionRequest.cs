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
using System;
using Newtonsoft.Json;
using RiftDrive.Shared.Model;

namespace RiftDrive.Shared.Message {
	public sealed class ReviveCrewActionRequest {

		[JsonConstructor]
		public ReviveCrewActionRequest(
			Id<Game> gameId,
			Id<Mothership> mothershipId,
			Id<MothershipModule> moduleId,
			Id<MothershipModuleAction> actionId
		) {
			GameId = gameId;
			MothershipId = mothershipId;
			ModuleId = moduleId;
			ActionId = actionId;
		}

		public Id<Game> GameId { get; }

		public Id<Mothership> MothershipId { get; }

		public Id<MothershipModule> ModuleId { get; }

		public Id<MothershipModuleAction> ActionId { get; }
	}
}
