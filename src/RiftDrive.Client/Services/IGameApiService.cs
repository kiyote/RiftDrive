using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiftDrive.Client.Model;

namespace RiftDrive.Client.Services {
	public interface IGameApiService {
		Task<Game> CreateGame( string name );

		Task<IEnumerable<Game>> GetGames();
	}
}
