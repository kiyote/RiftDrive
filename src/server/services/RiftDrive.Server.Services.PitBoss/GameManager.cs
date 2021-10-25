using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using RiftDrive.Common.Model;

namespace RiftDrive.Server.Services.PitBoss {

	[SuppressMessage( "Performance", "CA1812", Justification = "Dependency Injection" )]
	internal sealed class GameManager : IGameManager {

		private readonly IGameRepository _gameRepository;
		private readonly IMissionRepository _MissionRepository;

		public GameManager(
			IGameRepository gameRepository,
			IMissionRepository missionRepository
		) {
			_gameRepository = gameRepository;
			_MissionRepository = missionRepository;
		}


		async Task<IEnumerable<Game>> IGameManager.GetActiveGamesAsync( Id<User> userId ) {
			return await _gameRepository.GetUserGamesAsync( userId ).ConfigureAwait( false );
		}

		async Task<IEnumerable<Game>> IGameManager.GetAvailableGamesAsync(Id<User> userId) {
			IEnumerable<Game> activeGames = await _gameRepository.GetUserGamesAsync( userId ).ConfigureAwait( false );
			IEnumerable<Game> availableGames = await _gameRepository.GetAwaitingPlayersGamesAsync().ConfigureAwait( false );

			return availableGames.Except( activeGames );
		}

		async Task<Game> IGameManager.CreateGameAsync( Id<Game> gameId, string name, DateTime createdOn, GameState state ) {
			return await _gameRepository.CreateGameAsync( gameId, name, createdOn, state ).ConfigureAwait( false );
		}

		async Task IGameManager.DeleteGameAsync( Id<Game> gameId ) {
			await _gameRepository.DeleteGameAsync( gameId ).ConfigureAwait( false );
			await _MissionRepository.DeleteMissionsAsync( gameId ).ConfigureAwait( false );
		}

		async Task IGameManager.StartGameAsync( Id<Game> gameId ) {
			await _gameRepository.StartGameAsync( gameId ).ConfigureAwait( false );
		}

		async Task IGameManager.JoinGameAsync( Id<Game> gameId, Id<User> userId, string name ) {
			await _gameRepository.JoinGameAsync( gameId, userId, name, DateTime.UtcNow ).ConfigureAwait( false );
		}

		async Task<Game> IGameManager.GetGameAsync( Id<Game> gameId ) {
			return await _gameRepository.GetGameAsync( gameId ).ConfigureAwait( false );
		}
	}
}
