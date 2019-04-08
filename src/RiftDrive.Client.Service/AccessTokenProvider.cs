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
using RiftDrive.Client.Model;

namespace RiftDrive.Client.Service {
	internal sealed class AccessTokenProvider : IAccessTokenProvider {

		private readonly IAppState _state;
		private readonly ITokenService _tokenService;

		public AccessTokenProvider(
			IAppState state,
			ITokenService tokenService
		) {
			_state = state;
			_tokenService = tokenService;
		}

		public async Task SetTokens( string accessToken, string refreshToken, DateTime expiresAt ) {
			await _state.SetAccessToken( accessToken );
			await _state.SetRefreshToken( refreshToken );
			await _state.SetTokensExpireAt( expiresAt );
		}

		async Task<string> IAccessTokenProvider.GetJwtToken() {
			if( await _state.GetTokensExpireAt() < DateTimeOffset.Now ) {
				AuthorizationToken? tokens = await _tokenService.RefreshToken( await _state.GetAccessToken() );
				if( tokens != default ) {
					await SetTokens( tokens.access_token, tokens.refresh_token, DateTime.UtcNow.AddSeconds( tokens.expires_in ) );
				}
			}
			return await _state.GetAccessToken();
		}

		async Task IAccessTokenProvider.ClearTokens() {
			await _state.SetAccessToken( "" );
			await _state.SetRefreshToken( "" );
			await _state.SetTokensExpireAt( DateTime.MinValue );
		}
	}
}
