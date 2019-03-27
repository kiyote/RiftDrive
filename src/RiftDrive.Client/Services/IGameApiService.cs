using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiftDrive.Client.Model;
using RiftDrive.Shared;

namespace RiftDrive.Client.Services {
	public interface IGameApiService {
		Task<Game> CreateGame( string name );

		Task<IEnumerable<Game>> GetGames();

		Task<IEnumerable<Player>> GetPlayers( Id<Game> gameId );
	}
}
