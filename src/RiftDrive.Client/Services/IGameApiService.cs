using System.Collections.Generic;
using System.Threading.Tasks;
using RiftDrive.Client.Model;
using RiftDrive.Shared;

namespace RiftDrive.Client.Services {
	public interface IGameApiService {
		Task<Game> CreateGame( string gameName, string playerName );

		Task<IEnumerable<Game>> GetGames();

		Task<IEnumerable<Player>> GetPlayers( Id<Game> gameId );

		Task<Game> GetGame( Id<Game> gameId );
	}
}
