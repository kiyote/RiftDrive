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
using RiftDrive.Shared.Model;

namespace RiftDrive.Client.State {
	internal sealed class CurrentGameState: ICurrentGameState {

		public CurrentGameState() {
			Modules = new List<MothershipAttachedModule>();
			Crew = new List<Actor>();
			ActionLog = new List<string>();
		}

		public CurrentGameState( Game game, Mothership mothership, IEnumerable<MothershipAttachedModule> modules, IEnumerable<Actor> crew, IEnumerable<string> actionLog ) {
			Game = game;
			Mothership = mothership;
			Modules = modules;
			Crew = crew;
			ActionLog = actionLog;
		}

		public Game Game { get; }

		public Mothership Mothership { get; }

		public IEnumerable<MothershipAttachedModule> Modules { get; }

		public IEnumerable<Actor> Crew { get; }

		public IEnumerable<string> ActionLog { get; }

	}
}
