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
using System.Collections.Generic;
using System.Threading.Tasks;
using RiftDrive.Server.Model;
using RiftDrive.Server.Repository;
using RiftDrive.Shared;
using RiftDrive.Shared.Provider;

namespace RiftDrive.Server.Service {
	internal sealed class GameService : IGameService {

		private readonly IGameRepository _gameRepository;
		private readonly IPlayerRepository _playerRepository;
		private readonly IActorRepository _actorRepository;
		private readonly IMothershipRepository _mothershipRepository;
		private readonly INameProvider _nameProvider;

		public GameService(
			IGameRepository gameRepository,
			IPlayerRepository playerRepository,
			IActorRepository actorRepository,
			IMothershipRepository mothershipRepository,
			INameProvider nameProvider
		) {
			_gameRepository = gameRepository;
			_playerRepository = playerRepository;
			_actorRepository = actorRepository;
			_mothershipRepository = mothershipRepository;
			_nameProvider = nameProvider;
		}

		async Task<IEnumerable<Game>> IGameService.GetGames( Id<User> userId ) {
			return await _gameRepository.GetGames( userId );
		}

		async Task<Game> IGameService.GetGame( Id<Game> gameId ) {
			return await _gameRepository.GetGame( gameId );
		}

		async Task<Game> IGameService.StartGame( Id<Game> gameId ) {
			return await _gameRepository.StartGame( gameId );
		}

		async Task<Game> IGameService.CreateGame( CreateGameConfiguration config ) {
			Game game = await _gameRepository.Create( new Id<Game>(), config.GameName, config.CreatedOn );
			await _playerRepository.Create( game.Id, new Id<Player>(), config.CreatedBy, config.PlayerName, config.CreatedOn );

			Mothership mothership = await _mothershipRepository.CreateMothership( game.Id, new Id<Mothership>(), _nameProvider.CreateMothershipName(), 4, 10, config.CreatedOn );
			await _mothershipRepository.CreateModule( mothership.Id, MothershipModule.Hanger.Id, 5, config.CreatedOn );
			await _mothershipRepository.CreateModule( mothership.Id, MothershipModule.Cryogenics.Id, 5, config.CreatedOn );

			await _actorRepository.Create( game.Id, new Id<Actor>(), _nameProvider.CreateActorName(), Role.Command, 1, 1, 1, config.CreatedOn );
			await _actorRepository.Create( game.Id, new Id<Actor>(), _nameProvider.CreateActorName(), Role.Engineer, 1, 1, 1, config.CreatedOn );
			await _actorRepository.Create( game.Id, new Id<Actor>(), _nameProvider.CreateActorName(), Role.Science, 1, 1, 1, config.CreatedOn );
			await _actorRepository.Create( game.Id, new Id<Actor>(), _nameProvider.CreateActorName(), Role.Security, 1, 1, 1, config.CreatedOn );

			return game;
		}

		async Task<IEnumerable<Player>> IGameService.GetPlayers( Id<Game> gameId ) {
			return await _playerRepository.GetPlayers( gameId );
		}

		async Task IGameService.DeleteGame( Id<Game> gameId ) {
			IEnumerable<Actor> actors = await _actorRepository.GetActors( gameId );
			foreach( Actor actor in actors ) {
				await _actorRepository.Delete( gameId, actor.Id );
			}
			await _mothershipRepository.DeleteMothership( gameId );
			IEnumerable<Player> players = await _playerRepository.GetPlayers( gameId );
			foreach( Player player in players ) {
				await _playerRepository.Delete( gameId, player.Id );
			}
			await _gameRepository.Delete( gameId );
		}

		async Task<Mothership> IGameService.GetMothership(Id<Game> gameId) {
			return await _mothershipRepository.GetMothership( gameId );
		}
	}
}
