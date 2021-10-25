using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using RiftDrive.Common.Model;

namespace RiftDrive.Server.Services.PitBoss {

	[SuppressMessage( "Performance", "CA1812", Justification = "Dependency injection" )]
	internal sealed class PitBossService : IPitBossService {

		private readonly IGameManager _gameManager;

		public PitBossService(
			IGameManager gameManager
		) {
			_gameManager = gameManager;
		}


		async Task<Game> IPitBossService.CreateGameAsync( Id<User> userId, string gameName, string playerName ) {
			Game game = await _gameManager.CreateGameAsync( new Id<Game>(), gameName, DateTime.UtcNow, GameState.WaitingForPlayers ).ConfigureAwait( false );
			await _gameManager.JoinGameAsync( game.Id, userId, playerName ).ConfigureAwait( false );

			return game;
		}

		async Task<IEnumerable<Game>> IPitBossService.GetAvailableGamesAsync( Id<User> userId ) {
			return await _gameManager.GetAvailableGamesAsync( userId ).ConfigureAwait( false );
		}

		async Task<IEnumerable<Game>> IPitBossService.GetActiveGamesAsync( Id<User> userId ) {
			return await _gameManager.GetActiveGamesAsync( userId ).ConfigureAwait( false );
		}

		async Task IPitBossService.DeleteGameAsync( Id<Game> gameId ) {
			await _gameManager.DeleteGameAsync( gameId ).ConfigureAwait( false );
		}

		async Task<Game> IPitBossService.GetGameAsync( Id<Game> gameId ) {
			return await _gameManager.GetGameAsync( gameId ).ConfigureAwait( false );
		}
	}
}
