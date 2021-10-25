using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RiftDrive.Common.Model;

namespace RiftDrive.Server.Services.PitBoss {
	internal interface IGameRepository {
		Task<IEnumerable<Game>> GetUserGamesAsync( Id<User> userId );

		Task<Game> CreateGameAsync( Id<Game> gameId, string name, DateTime createdOn, GameState state );

		Task<IEnumerable<Game>> GetAwaitingPlayersGamesAsync();

		Task DeleteGameAsync( Id<Game> gameId );

		Task<Game> GetGameAsync( Id<Game> gameId );

		Task StartGameAsync( Id<Game> gameId );

		Task JoinGameAsync( Id<Game> gameId, Id<User> userId, string name, DateTime createdOn );
	}
}
