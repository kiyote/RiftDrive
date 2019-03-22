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
using Microsoft.JSInterop;

namespace RiftDrive.Client {
	internal sealed class AppState : IAppState {

		private readonly IJSRuntime _js;

		public AppState( IJSRuntime jsRuntime ) {
			_js = jsRuntime;
		}

		public event EventHandler OnStateChanged;

		public async Task<string> GetUsername() {
			var value = await _js.InvokeAsync<string>( "appState.getItem", "Username" );
			return value ?? string.Empty;
		}

		public async Task SetUsername( string value ) {
			await _js.InvokeAsync<string>( "appState.setItem", "Username", value );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public async Task<string> GetAccessToken() {
			return await _js.InvokeAsync<string>( "appState.getItem", "AccessToken" );
		}

		public async Task SetAccessToken( string value ) {
			await _js.InvokeAsync<string>( "appState.setItem", "AccessToken", value );
		}

		public async Task<string> GetRefeshToken() {
			return await _js.InvokeAsync<string>( "appState.getItem", "RefreshToken" );
		}

		public async Task SetRefreshToken( string value ) {
			await _js.InvokeAsync<string>( "appState.setItem", "RefreshToken", value );
		}

		public async Task<DateTime> GetTokensExpireAt() {
			var value = await _js.InvokeAsync<string>( "appState.getItem", "TokensExpireAt" );
			if( value != default ) {
				return DateTime.Parse( value ).ToUniversalTime();
			}

			return DateTime.MinValue.ToUniversalTime();
		}

		public async Task SetTokensExpireAt( DateTime value ) {
			await _js.InvokeAsync<string>( "appState.setItem", "TokensExpireAt", value.ToString( "o" ) );
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}

		public async Task<bool> GetIsAuthenticated() {
			var tokensExpireAt = await GetTokensExpireAt();
			return tokensExpireAt > DateTime.UtcNow;
		}
	}
}
