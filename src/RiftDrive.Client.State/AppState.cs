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
using System.Threading.Tasks;
using RiftDrive.Shared;

namespace RiftDrive.Client.State {
	internal sealed class AppState : IAppState {

		private readonly IStateStorage _storage;

		public AppState( IStateStorage storage ) {
			_storage = storage;

			Authentication = new AuthenticationState();
			Validation = new ValidationState();
			GamePlay = new GamePlayState();
		}

		public event EventHandler OnStateChanged;

		public async Task Initialize() {
			Authentication = await AuthenticationState.InitialState( _storage );
		}

		public IAuthenticationState Authentication { get; private set; }

		public IValidationState Validation { get; private set; }

		public IGamePlayState GamePlay { get; private set; }

		public async Task SetTokens( string accessToken, string refreshToken, DateTime tokensExpireAt ) {
			await _storage.Set( "AccessToken", accessToken );
			await _storage.Set( "RefreshToken", refreshToken );
			await _storage.Set( "TokensExpireAt", tokensExpireAt.ToUniversalTime().ToString( "o" ) );
			Authentication = new AuthenticationState( Authentication, accessToken, refreshToken, tokensExpireAt );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public async Task ClearTokens() {
			await _storage.Set( "AccessToken", "" );
			await _storage.Set( "RefreshToken", "" );
			await _storage.Set( "TokensExpireAt", DateTime.MinValue.ToUniversalTime().ToString( "o" ) );
			Authentication = new AuthenticationState();
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public async Task SetUserInformation( string username, string name ) {
			await _storage.Set( "Name", name );
			await _storage.Set( "Username", username );
			Authentication = new AuthenticationState( Authentication, username, name );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public Task UpdateValidationProgress( string message, int progress ) {
			Validation = new ValidationState( Validation, message, progress );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
			return Task.CompletedTask;
		}

		public Task SetGame( Game  game ) {
			GamePlay = new GamePlayState( game );
			return Task.CompletedTask;
		}
	}
}
