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
using System.Collections.Generic;
using System.Threading.Tasks;
using RiftDrive.Shared.Model;
using RiftDrive.Shared.Model.Client;

namespace RiftDrive.Client.State {
	public sealed class NullAppState : IAppState {

		public static IAppState Instance = new NullAppState();

		bool IAppState.IsInitialized => false;

		IAuthenticationState IAppState.Authentication => throw new NotImplementedException();

		ICurrentGameState IAppState.CurrentGame => throw new NotImplementedException();

		IMissionState IAppState.CurrentMission => throw new NotImplementedException();

		IAdministrationState IAppState.Administration => throw new NotImplementedException();

		event EventHandler IAppState.OnStateChanged {
			add {
				throw new NotImplementedException();
			}

			remove {
				throw new NotImplementedException();
			}
		}

		Task IAppState.ClearState() {
			throw new NotImplementedException();
		}

		Task IAppState.Initialize() {
			throw new NotImplementedException();
		}

		Task IAppState.Update( IAuthenticationState initial, ClientUser? user ) {
			throw new NotImplementedException();
		}

		Task IAppState.Update( IAuthenticationState initial, string? idToken, string? refreshToken, DateTime tokensExpireAt ) {
			throw new NotImplementedException();
		}

		Task IAppState.Update( ICurrentGameState initial, Game? game ) {
			throw new NotImplementedException();
		}

		Task IAppState.Update( ICurrentGameState initial, IEnumerable<Actor> crew ) {
			throw new NotImplementedException();
		}

		Task IAppState.Update( ICurrentGameState initial, Mothership? mothership ) {
			throw new NotImplementedException();
		}

		Task IAppState.Update( ICurrentGameState initial, IEnumerable<MothershipAttachedModule> modules ) {
			throw new NotImplementedException();
		}

		Task IAppState.Update( ICurrentGameState initial, IEnumerable<string> actionLog ) {
			throw new NotImplementedException();
		}

		Task IAppState.Update( IMissionState initial, Mission? mission ) {
			throw new NotImplementedException();
		}

		Task IAppState.Update( IMissionState initial, IEnumerable<Actor> crew ) {
			throw new NotImplementedException();
		}

		Task IAppState.Update( IMissionState initial, EncounterOutcome? encounterOutcome ) {
			throw new NotImplementedException();
		}

		Task IAppState.Update( ICurrentGameState initial, Game? game, IEnumerable<Actor> crew, Mothership? mothership, IEnumerable<MothershipAttachedModule> modules ) {
			throw new NotImplementedException();
		}

		Task IAppState.Update( ICurrentGameState initial, Mothership? mothership, IEnumerable<MothershipAttachedModule> modules, IEnumerable<string> actionLog ) {
			throw new NotImplementedException();
		}

		Task IAppState.Update( IAdministrationState initial, IEnumerable<Game> games ) {
			throw new NotImplementedException();
		}

		Task IAppState.Update( IAdministrationState initial, ClientUser? profile ) {
			throw new NotImplementedException();
		}
	}
}
