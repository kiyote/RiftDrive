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
using System.Threading.Tasks;
using RiftDrive.Shared;

namespace RiftDrive.Client.State {
	internal sealed class GamePlayState: IGamePlayState {

		public GamePlayState() {
			Game = default;
			Mothership = default;
			Crew = new List<Actor>();
			Modules = new List<MothershipAttachedModule>();
		}

		public GamePlayState( IGamePlayState initial, Game game) {
			Game = game;
			Mothership = Mothership;
			Crew = initial.Crew;
			Modules = initial.Modules;
		}

		public GamePlayState( IGamePlayState initial, Mothership mothership ) {
			Game = initial.Game;
			Mothership = mothership;
			Crew = initial.Crew;
			Modules = initial.Modules;
		}

		public GamePlayState( IGamePlayState initial, IEnumerable<Actor> crew ) {
			Game = initial.Game;
			Mothership = initial.Mothership;
			Crew = crew;
			Modules = initial.Modules;
		}

		public GamePlayState( IGamePlayState initial, IEnumerable<MothershipAttachedModule> modules ) {
			Game = initial.Game;
			Mothership = initial.Mothership;
			Crew = initial.Crew;
			Modules = modules;
		}

		public Game Game { get; }

		public Mothership Mothership { get; }

		public IEnumerable<Actor> Crew { get; }

		public IEnumerable<MothershipAttachedModule> Modules { get; }

		public static Task<GamePlayState> InitialState( IStateStorage storage ) {
			return Task.FromResult( new GamePlayState() );
		}
	}
}
