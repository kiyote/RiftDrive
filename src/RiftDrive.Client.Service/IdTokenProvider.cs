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
	internal sealed class IdTokenProvider : IIdTokenProvider {

		private readonly IAppState _state;
		private readonly ITokenService _tokenService;

		public IdTokenProvider(
			IAppState state,
			ITokenService tokenService
		) {
			_state = state;
			_tokenService = tokenService;
		}

		async Task<string> IIdTokenProvider.GetIdToken() {

			if ((_state.Authentication.IdToken == default) || (_state.Authentication.RefreshToken == default)) {
				throw new InvalidOperationException();
			}

			if( _state.Authentication.TokensExpireAt < DateTimeOffset.Now ) {
				AuthorizationToken? tokens = await _tokenService.RefreshToken( _state.Authentication.RefreshToken );
				if( tokens != default ) {
					await _state.Update( _state.Authentication, tokens.id_token, tokens.refresh_token, DateTime.UtcNow.AddSeconds( tokens.expires_in ) );
				}
			}
			return _state.Authentication.IdToken;
		}
	}
}
