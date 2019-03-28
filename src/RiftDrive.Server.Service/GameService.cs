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
using RiftDrive.Server.Repository;
using RiftDrive.Shared;

namespace RiftDrive.Server.Service {
	internal sealed class GameService : IGameService {

		private readonly IGameRepository _gameRepository;
		private readonly IPlayerRepository _playerRepository;

		public GameService(
			IGameRepository gameRepository,
			IPlayerRepository playerRepository
		) {
			_gameRepository = gameRepository;
			_playerRepository = playerRepository;
		}

		async Task<IEnumerable<Game>> IGameService.GetGames( Id<User> userId ) {
			return await _gameRepository.GetGames( userId );
		}

		async Task<Game> IGameService.GetGame( Id<Game> gameId ) {
			return await _gameRepository.GetGame( gameId );
		}

		async Task<Player> IGameService.GetPlayer( Id<Game> gameId, Id<User> userId ) {
			var players = await _playerRepository.GetPlayers( gameId );
			return players.FirstOrDefault( p => p.UserId == userId );
		}

		async Task<Game> IGameService.CreateGame( Id<Game> gameId, string name, DateTime createdOn ) {
			return await _gameRepository.Create( gameId, name, createdOn );
		}

		async Task<Player> IGameService.AddPlayer( Id<Game> gameId, Id<Player> playerId, Id<User> userId, string name, DateTime createdOn ) {
			return await _playerRepository.Create( gameId, playerId, userId, name, createdOn );
		}

		async Task IGameService.RemovePlayer( Id<Game> gameId, Id<Player> playerId ) {
			await _playerRepository.Delete( gameId, playerId );
		}

		async Task<IEnumerable<Player>> IGameService.GetPlayers( Id<Game> gameId ) {
			return await _playerRepository.GetPlayers( gameId );
		}

		async Task IGameService.DeleteGame(Id<Game> gameId) {
			var players = await _playerRepository.GetPlayers( gameId );
			foreach (var player in players) {
				await _playerRepository.Delete( gameId, player.Id );
			}
			await _gameRepository.Delete( gameId );
		}
	}
}
