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
using Newtonsoft.Json;
using RiftDrive.Client.Model;
using RiftDrive.Shared;

namespace RiftDrive.Client.State {
	internal sealed class GamePlayState: IGamePlayState {

		public GamePlayState() {
			Game = default;
			Mothership = default;
			Crew = new List<Actor>();
			Modules = new List<MothershipAttachedModule>();
			Players = new List<Player>();
		}

		[JsonConstructor]
		public GamePlayState(
			Game game,
			Mothership mothership,
			IEnumerable<Actor> crew,
			IEnumerable<MothershipAttachedModule> modules,
			IEnumerable<Player> players
		) {
			Game = game;
			Mothership = mothership;
			Crew = crew;
			Modules = modules;
			Players = players;
		}

		public Game Game { get; }

		public Mothership Mothership { get; }

		public IEnumerable<Actor> Crew { get; }

		public IEnumerable<MothershipAttachedModule> Modules { get; }

		public IEnumerable<Player> Players { get; }
	}
}
