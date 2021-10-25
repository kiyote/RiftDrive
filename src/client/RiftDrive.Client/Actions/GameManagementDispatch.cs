using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiftDrive.Client.Hubs;
using RiftDrive.Common.Model;

namespace RiftDrive.Client.Actions {
	internal sealed class GameManagementDispatch : IGameManagementDispatch {

		private readonly IClientHub _hub;

		public GameManagementDispatch(
			IClientHub clientHub
		) {
			_hub = clientHub;
		}

		Task IGameManagementDispatch.InitializeAsync() {
			return Task.CompletedTask;
		}
		async Task IGameManagementDispatch.CreateGameAsync( string gameName, string playerName ) {
			await _hub.CreateGameAsync( gameName, playerName );
		}

		async Task IGameManagementDispatch.DeleteGameAsync( Id<Game> gameId ) {
			await _hub.DeleteGameAsync( gameId );
		}

		async Task IGameManagementDispatch.LoadGameAsync( Id<Game> gameId ) {
			await _hub.LoadGameAsync( gameId );
		}
	}
}
