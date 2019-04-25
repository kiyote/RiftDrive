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

		private readonly IUserApiService _userService;
		private readonly IGameApiService _gameService;
		private readonly IAppState _state;

		public Dispatch(
			IAppState state,
			IGameApiService gameService,
			IUserApiService userService
		) {
			_state = state;
			_gameService = gameService;
			_userService = userService;
		}

		public async Task PlayGame( Id<Game> gameId ) {
			Game game = await _gameService.GetGame( gameId );
			Mothership mothership = await _gameService.GetMothership( gameId );
			IEnumerable<MothershipAttachedModule> modules = await _gameService.GetMothershipModules( gameId, mothership.Id );
			IEnumerable<Actor> crew = await _gameService.GetCrew( gameId );
			IEnumerable<Player> players = await _gameService.GetPlayers( gameId );
			await _state.SetPlayGameState( game, mothership, crew, modules, players );
		}

		public async Task ViewGame( Id<Game> gameId ) {
			Game game = await _gameService.GetGame( gameId );
			IEnumerable<Player> players = await _gameService.GetPlayers( gameId );
			await _state.SetGame( game, players );
		}

		public async Task ViewProfile() {
			User user = await _userService.GetUserInformation();
			IEnumerable<Game> games = await _gameService.GetGames();
			await _state.SetProfileUser( user );
			await _state.SetGames( games );
		}

		public async Task StartGame( Id<Game> gameId, string message ) {
			await _gameService.StartGame( gameId, message );
		}
	}
}
