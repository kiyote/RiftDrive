using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiftDrive.Common.Model;

namespace RiftDrive.Server.Services {
	public interface ISecurePitBossService {

		Task<Game> CreateGameAsync( Id<User> caller, Id<User> userId, string gameName, string playerName );

		Task<IEnumerable<Game>> GetAvailableGamesAsync( Id<User> caller, Id<User> userId );

		Task<IEnumerable<Game>> GetActiveGamesAsync( Id<User> caller, Id<User> userId );

		Task DeleteGameAsync( Id<User> caller, Id<Game> gameId );

		Task<Game> GetGameAsync( Id<User> caller, Id<Game> gameId );
	}
}
