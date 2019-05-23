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

namespace RiftDrive.Client.State {
	internal sealed class AppState : IAppState {

		private readonly IStateStorage _storage;

		public AppState(
			IStateStorage storage
		) {
			IsInitialized = false;
			_storage = storage;

			Authentication = new AuthenticationState();
			CurrentGame = new CurrentGameState();
		}

		public event EventHandler OnStateChanged;

		public bool IsInitialized { get; private set; }

		public IAuthenticationState Authentication { get; private set; }

		public ICurrentGameState CurrentGame { get; private set; }

		public async Task Initialize() {
			Authentication = await _storage.Get<AuthenticationState>( "State::Authentication" ) ?? new AuthenticationState();
			IsInitialized = true;
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public async Task ClearState() {
			await _storage.Set( "State::Authentication", "" );
			await Initialize();
		}

		public async Task Update( IAuthenticationState initial, string accessToken, string refreshToken, DateTime tokensExpireAt ) {
			Authentication = new AuthenticationState( Authentication.User, accessToken, refreshToken, tokensExpireAt );
			await _storage.Set( "State::Authentication", Authentication );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public async Task Update( IAuthenticationState initial, ClientUser user ) {
			Authentication = new AuthenticationState( user, initial.AccessToken, initial.RefreshToken, initial.TokensExpireAt );
			await _storage.Set( "State::Authentication", Authentication );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public async Task Update( ICurrentGameState initial, Game game ) {
			CurrentGame = new CurrentGameState( game, initial.Mothership, initial.Modules, initial.Crew, initial.ActionLog, initial.Mission );
			await _storage.Set( "State::CurrentGame", CurrentGame );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public async Task Update( ICurrentGameState initial, IEnumerable<Actor> crew ) {
			CurrentGame = new CurrentGameState( initial.Game, initial.Mothership, initial.Modules, crew, initial.ActionLog, initial.Mission );
			await _storage.Set( "State::CurrentGame", CurrentGame );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public async Task Update( ICurrentGameState initial, Mothership mothership ) {
			CurrentGame = new CurrentGameState( initial.Game, mothership, initial.Modules, initial.Crew, initial.ActionLog, initial.Mission );
			await _storage.Set( "State::CurrentGame", CurrentGame );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public async Task Update( ICurrentGameState initial, IEnumerable<MothershipAttachedModule> modules ) {
			CurrentGame = new CurrentGameState( initial.Game, initial.Mothership, modules, initial.Crew, initial.ActionLog, initial.Mission );
			await _storage.Set( "State::CurrentGame", CurrentGame );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public async Task Update( ICurrentGameState initial, IEnumerable<string> actionLog ) {
			CurrentGame = new CurrentGameState( initial.Game, initial.Mothership, initial.Modules, initial.Crew, actionLog, initial.Mission );
			await _storage.Set( "State::CurrentGame", CurrentGame );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public async Task Update( ICurrentGameState initial, Mission mission ) {
			CurrentGame = new CurrentGameState( initial.Game, initial.Mothership, initial.Modules, initial.Crew, initial.ActionLog, mission );
			await _storage.Set( "State::CurrentGame", CurrentGame );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}
	}
}
