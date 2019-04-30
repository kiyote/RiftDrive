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
using RiftDrive.Client.Service;
using RiftDrive.Client.State;

namespace RiftDrive.Client.Action {
	public class Dispatch : IDispatch {

		private readonly ITokenService _tokenService;
		private readonly IUserApiService _userService;
		private readonly IGameApiService _gameService;
		private readonly IAppState _state;

		public Dispatch(
			IAppState state,
			IGameApiService gameService,
			IUserApiService userService,
			ITokenService tokenService
		) {
			_state = state;
			_gameService = gameService;
			_userService = userService;
			_tokenService = tokenService;
		}

		public async Task LogInUser( string code ) {
			List<string> messages = new List<string>();
			messages.Add( "...retrieving token..." );
			await _state.Update( _state.Authentication, messages, 10 );
			AuthorizationToken tokens = await _tokenService.GetToken( code );
			if( tokens == default ) {
				//TODO: Do something here
				throw new InvalidOperationException();
			}
			await _state.Update( _state.Authentication, tokens.access_token, tokens.refresh_token, DateTime.UtcNow.AddSeconds( tokens.expires_in ) );

			messages.Add( "...recording login..." );
			await _state.Update( _state.Authentication, messages, 50 );
			await _userService.RecordLogin();

			messages.Add( "...loading user information..." );
			await _state.Update( _state.Authentication, messages, 75 );
			User userInfo = await _userService.GetUserInformation();
			await _state.Update( _state.Authentication, userInfo );
			await _state.Update( _state.Authentication, new List<string>(), 100 );
		}
	}
}
