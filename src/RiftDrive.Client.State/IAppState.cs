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
using RiftDrive.Client.Model;
using RiftDrive.Shared;

namespace RiftDrive.Client.State {
	public interface IAppState {
		event EventHandler OnStateChanged;

		bool IsInitialized { get; }

		IAuthenticationState Authentication { get; }

		IValidationState Validation { get; }

		IGamePlayState GamePlay { get; }

		IUserInformationState UserInformation { get; }

		Task Initialize();

		Task ClearState();

		Task Update( IValidationState initial, string message, int progress );

		Task Update( IAuthenticationState initial, string username, string name );

		Task Update( IAuthenticationState initial, string accessToken, string refreshToken, DateTime tokensExpireAt );

		Task Update( IGamePlayState initial, Game game, IEnumerable<Player> players );

		Task Update( IGamePlayState initial, Game game, Mothership mothership, IEnumerable<Actor> crew, IEnumerable<MothershipAttachedModule> module, IEnumerable<Player> players );

		Task Update( IUserInformationState initial, User user );

		Task Update( IUserInformationState initial, IEnumerable<Game> games );
	}
}
