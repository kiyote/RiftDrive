using System.Collections.Generic;
using System.Threading.Tasks;
using RiftDrive.Client.Model;
using RiftDrive.Shared;

namespace RiftDrive.Client.Service {
	public interface IGameApiService {
		Task<Game> CreateGame( string gameName, string playerName );

		Task<Game> StartGame( Id<Game> gameId, string message );

		Task<IEnumerable<Game>> GetGames();

		Task<IEnumerable<Player>> GetPlayers( Id<Game> gameId );

		Task<Game?> GetGame( Id<Game> gameId );

		Task<Mothership> GetMothership( Id<Game> gameId );

		Task<IEnumerable<Actor>> GetCrew( Id<Game> gameId );
	}
}
