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
	internal sealed class AppState : IAppState {

		private readonly IStateStorage _storage;

		public AppState(
			IStateStorage storage
		) {
			IsInitialized = false;
			_storage = storage;

			Authentication = new AuthenticationState();
			Validation = new ValidationState();
			GamePlay = new GamePlayState();
			UserInformation = new UserInformationState();
		}

		public event EventHandler OnStateChanged;

		public async Task Initialize() {
			Authentication = await _storage.Get<AuthenticationState>( "State::Authentication" ) ?? new AuthenticationState();
			GamePlay = await _storage.Get<GamePlayState>( "State::GamePlay" ) ?? new GamePlayState();
			UserInformation = await _storage.Get<UserInformationState>( "State::UserInformation" ) ?? new UserInformationState();
			Validation = await _storage.Get<ValidationState>( "State::Validation" ) ?? new ValidationState();
			IsInitialized = true;
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public bool IsInitialized { get; private set; }

		public IAuthenticationState Authentication { get; private set; }

		public IValidationState Validation { get; private set; }

		public IGamePlayState GamePlay { get; private set; }

		public IUserInformationState UserInformation { get; private set; }

		public async Task Update( IAuthenticationState initial, string accessToken, string refreshToken, DateTime tokensExpireAt ) {
			Authentication = new AuthenticationState( Authentication.Username, Authentication.Name, accessToken, refreshToken, tokensExpireAt );
			await _storage.Set( "State::Authentication", Authentication );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public async Task ClearState() {
			await _storage.Set( "State::Authentication", "" );
			await _storage.Set( "State::UserInformation", "" );
			await _storage.Set( "State::GamePlay", "" );
			await _storage.Set( "State::Validation", "" );
			await Initialize();
		}

		public async Task Update( IAuthenticationState initial, string username, string name ) {
			Authentication = new AuthenticationState( username, name, initial.AccessToken, initial.RefreshToken, initial.TokensExpireAt );
			await _storage.Set( "State::Authentication", Authentication );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public async Task Update( IValidationState initial, string message, int progress ) {
			List<string> messages = new List<string>( initial.Messages );
			messages.Add( message );
			Validation = new ValidationState( messages, progress );
			await _storage.Set( "State::Validation", Authentication );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public async Task Update(
			IGamePlayState initial,
			Game game,
			Mothership mothership,
			IEnumerable<Actor> crew,
			IEnumerable<MothershipAttachedModule> modules,
			IEnumerable<Player> players
		) {
			GamePlay = new GamePlayState( game, mothership, crew, modules, players );
			await _storage.Set( "State::GamePlay", GamePlay );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public async Task Update(
			IGamePlayState initial,
			Game game,
			IEnumerable<Player> players
		) {
			GamePlay = new GamePlayState( game, initial.Mothership, initial.Crew, initial.Modules, players );
			await _storage.Set( "State::GamePlay", GamePlay );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public async Task Update( IUserInformationState initial, User user ) {
			UserInformation = new UserInformationState( user, initial.Games );
			await _storage.Set( "State::UserInformation", UserInformation );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public async Task Update( IUserInformationState initial, IEnumerable<Game> games ) {
			UserInformation = new UserInformationState( initial.User, games );
			await _storage.Set( "State::UserInformation", UserInformation );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}
	}
}
