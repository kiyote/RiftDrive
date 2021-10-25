using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RiftDrive.Common.Model;

namespace RiftDrive.Server.Services.PitBoss
{
    public interface IPitBossService
    {
		Task<Game> CreateGameAsync( Id<User> userId, string gameName, string playerName );

		Task<IEnumerable<Game>> GetAvailableGamesAsync( Id<User> userId );

		Task<IEnumerable<Game>> GetActiveGamesAsync( Id<User> userId );

		Task DeleteGameAsync( Id<Game> gameId );

		Task<Game> GetGameAsync( Id<Game> gameId );
    }
}
