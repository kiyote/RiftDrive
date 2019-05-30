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
using RiftDrive.Client.State;
using RiftDrive.Shared.Message;

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

		async Task<string> IAccessTokenProvider.GetJwtToken() {

			if (_state.Authentication.AccessToken == default) {
				throw new InvalidOperationException();
			}

			if( _state.Authentication.TokensExpireAt < DateTimeOffset.Now ) {
				AuthorizationToken tokens = await _tokenService.RefreshToken( _state.Authentication.AccessToken );
				if( tokens != default ) {
					await _state.Update( _state.Authentication, tokens.access_token, tokens.refresh_token, DateTime.UtcNow.AddSeconds( tokens.expires_in ) );
				}
			}
			return _state.Authentication.AccessToken;
		}
	}
}
