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
using RiftDrive.Shared.Model;
using RiftDrive.Shared.Model.Client;

namespace RiftDrive.Client.State {
	public interface IAppState {
		event EventHandler OnStateChanged;

		bool IsInitialized { get; }

		IAuthenticationState Authentication { get; }

		ICurrentGameState CurrentGame { get; }

		IMissionState CurrentMission { get; }

		IAdministrationState Administration { get; }

		Task Initialize();

		Task ClearState();

		Task Update( IAuthenticationState initial, ClientUser? user );

		Task Update( IAuthenticationState initial, string? idToken, string? refreshToken, DateTime tokensExpireAt );

		Task Update( ICurrentGameState initial, Game? game );

		Task Update( ICurrentGameState initial, IEnumerable<Actor> crew );

		Task Update( ICurrentGameState initial, Mothership? mothership );

		Task Update( ICurrentGameState initial, IEnumerable<MothershipAttachedModule> modules );

		Task Update( ICurrentGameState initial, IEnumerable<string> actionLog );

		Task Update( IMissionState initial, Mission? mission );

		Task Update( IMissionState initial, IEnumerable<Actor> crew );

		Task Update(
			ICurrentGameState initial,
			Game? game,
			IEnumerable<Actor> crew,
			Mothership? mothership,
			IEnumerable<MothershipAttachedModule> modules );

		Task Update(
			ICurrentGameState initial,
			Mothership? mothership,
			IEnumerable<MothershipAttachedModule> modules,
			IEnumerable<string> actionLog );

		Task Update(
			IAdministrationState initial,
			IEnumerable<Game> games );

		Task Update(
			IAdministrationState initial,
			ClientUser? profile );
	}
}
