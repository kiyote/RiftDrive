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
using RiftDrive.Shared;

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

		public async Task PlayGame( Id<Game> gameId ) {
			Console.WriteLine( "PlayGame" );
			Game game = await _gameService.GetGame( gameId );
			Mothership mothership = await _gameService.GetMothership( gameId );
			IEnumerable<MothershipAttachedModule> modules = await _gameService.GetMothershipModules( gameId, mothership.Id );
			IEnumerable<Actor> crew = await _gameService.GetCrew( gameId );
			IEnumerable<Player> players = await _gameService.GetPlayers( gameId );
			await _state.Update( _state.GamePlay, game, mothership, crew, modules, players );
		}

		public async Task ViewGame( Id<Game> gameId ) {
			Console.WriteLine( "ViewGame" );
			Game game = await _gameService.GetGame( gameId );
			IEnumerable<Player> players = await _gameService.GetPlayers( gameId );
			await _state.Update( _state.GamePlay, game, players );
		}

		public async Task ViewUserProfile() {
			Console.WriteLine( "ViewUserProfile" );
			if( _state.Authentication.IsAuthenticated) {
				User user = await _userService.GetUserInformation();
				await _state.Update( _state.UserInformation, user );
			}
		}

		public async Task ViewUserGames() {
			Console.WriteLine( "ViewUserGames" );
			if( _state.Authentication.IsAuthenticated) {
				IEnumerable<Game> games = await _gameService.GetGames();
				await _state.Update( _state.UserInformation, games );
			}
		}

		public async Task StartGame( Id<Game> gameId, string message ) {
			Console.WriteLine( "StartGame" );
			await _gameService.StartGame( gameId, message );
		}

		public async Task RetrieveTokens( string code ) {
			await _state.Update( _state.Validation, "...retrieving tokens...", 5 );
			AuthorizationToken tokens = await _tokenService.GetToken( code );
			if( tokens == default ) {
				//TODO: Do something here
				throw new InvalidOperationException();
			}
			await _state.Update( _state.Authentication, tokens.access_token, tokens.refresh_token, DateTime.UtcNow.AddSeconds( tokens.expires_in ) );
		}

		public async Task LogInUser() {
			await _state.Update( _state.Validation, "...recording login...", 50 );
			await _userService.RecordLogin();

			await _state.Update( _state.Validation, "...retrieving user information...", 75 );
			User userInfo = await _userService.GetUserInformation();

			await _state.Update( _state.Authentication, userInfo.Username, userInfo.Name );
		}
	}
}
