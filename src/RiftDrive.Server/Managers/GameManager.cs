﻿/*
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
using ClientPlayer = RiftDrive.Client.Model.Player;
using ClientMothership = RiftDrive.Client.Model.Mothership;

namespace RiftDrive.Server.Managers {
	public class GameManager {

		private readonly IGameService _gameService;

		public GameManager(
			IGameService gameService
		) {
			_gameService = gameService;
		}

		public async Task<Game> CreateGame( Id<User> userId, string gameName, string playerName ) {
			var config = new CreateGameConfiguration(
				userId,
				DateTime.UtcNow,
				gameName,
				playerName
				);
			Game game = await _gameService.CreateGame( config );

			return game;
		}

		public async Task<IEnumerable<Game>> GetGames( Id<User> userId ) {
			IEnumerable<Game> games = await _gameService.GetGames( userId );

			return games;
		}

		public async Task<Game> GetGame( Id<Game> gameId ) {
			Game game = await _gameService.GetGame( gameId );

			return game;
		}

		public async Task<Game> StartGame( Id<Game> gameId ) {
			Game game = await _gameService.StartGame( gameId );

			return game;
		}

		public async Task<IEnumerable<ClientPlayer>> GetPlayers( Id<Game> gameId ) {
			IEnumerable<Player> players = await _gameService.GetPlayers( gameId );

			return players.Select( p => ToClientPlayer( p ) );
		}

		public async Task<ClientMothership> GetMothership( Id<Game> gameId ) {
			Mothership mothership = await _gameService.GetMothership( gameId );
			return ToClientMothership( mothership );
		}

		public async Task<IEnumerable<Actor>> GetCrew( Id<Game> gameId ) {
			return await _gameService.GetCrew( gameId );
		}

		private ClientPlayer ToClientPlayer( Player player ) {
			return new ClientPlayer(
				new Id<ClientPlayer>( player.Id.Value ),
				player.GameId,
				player.Name );
		}

		private ClientMothership ToClientMothership( Mothership mothership ) {
			return new ClientMothership(
				new Id<ClientMothership>( mothership.Id.Value ),
				mothership.GameId,
				mothership.Name,
				mothership.AvailableCrew,
				mothership.RemainingFuel );
		}
	}
}
