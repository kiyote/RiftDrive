using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RiftDrive.Common.Model;

namespace RiftDrive.Server.Services.PitBoss {
	internal interface IGameManager {

		Task<IEnumerable<Game>> GetActiveGamesAsync( Id<User> userId );

		Task<IEnumerable<Game>> GetAvailableGamesAsync( Id<User> userId );

		Task<Game> CreateGameAsync( Id<Game> gameId, string name, DateTime createdOn, GameState state );

		Task DeleteGameAsync( Id<Game> gameId );

		Task StartGameAsync( Id<Game> gameId );

		Task JoinGameAsync( Id<Game> gameId, Id<User> userId, string name );

		Task<Game> GetGameAsync( Id<Game> gameId );
	}
}
