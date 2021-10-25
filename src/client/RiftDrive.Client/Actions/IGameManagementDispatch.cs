using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiftDrive.Common.Model;

namespace RiftDrive.Client.Actions {
	public interface IGameManagementDispatch {

		Task InitializeAsync();

		Task CreateGameAsync( string gameName, string playerName );

		Task DeleteGameAsync( Id<Game> gameId );

		Task LoadGameAsync( Id<Game> gameId );
	}
}
