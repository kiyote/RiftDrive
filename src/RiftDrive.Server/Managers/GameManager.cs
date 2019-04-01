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
using System.Linq;
using System.Threading.Tasks;
using RiftDrive.Server.Model;
using RiftDrive.Server.Service;
using RiftDrive.Shared;
using ClientGame = RiftDrive.Client.Model.Game;
using ClientPlayer = RiftDrive.Client.Model.Player;

namespace RiftDrive.Server.Managers {
	public class GameManager {

		private readonly IGameService _gameService;

		public GameManager(
			IGameService gameService
		) {
			_gameService = gameService;
		}

		public async Task<ClientGame> CreateGame(Id<User> userId, string gameName, string playerName) {
			Game game = await _gameService.CreateGame( new Id<Game>(), gameName, DateTime.UtcNow );
			await _gameService.AddPlayer( game.Id, new Id<Player>(), userId, playerName, DateTime.UtcNow );

			return ToClientGame( game );
		}

		public async Task<IEnumerable<ClientGame>> GetGames(Id<User> userId) {
			IEnumerable<Game> games = await _gameService.GetGames( userId );

			return games.Select( g => ToClientGame( g ) );
		}

		public async Task<ClientGame> GetGame( Id<Game> gameId ) {
			Game game = await _gameService.GetGame( gameId );

			return ToClientGame( game );
		}

		public async Task<ClientGame> StartGame( Id<Game> gameId ) {
			Game game = await _gameService.StartGame( gameId );

			return ToClientGame( game );
		}

		public async Task<IEnumerable<ClientPlayer>> GetPlayers(Id<Game> gameId) {
			IEnumerable<Player> players = await _gameService.GetPlayers( gameId );

			return players.Select( p => ToClientPlayer( p ) );
		}

		private ClientGame ToClientGame(Game game) {
			return new ClientGame(
				new Id<ClientGame>( game.Id.Value ),
				game.Name,
				game.CreatedOn,
				game.State );
		}

		private ClientPlayer ToClientPlayer(Player player) {
			return new ClientPlayer(
				new Id<ClientPlayer>( player.Id.Value ),
				new Id<ClientGame>( player.GameId.Value ),
				player.Name );
		}
	}
}
