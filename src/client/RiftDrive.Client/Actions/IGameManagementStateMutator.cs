using System;
using System.Collections.Generic;
using RiftDrive.Common.Model;

namespace RiftDrive.Client.Actions {
	public interface IGameManagementStateMutator {

		void AddAvailableGames( IEnumerable<Game> games );

		void AddActiveGames( IEnumerable<Game> games );

		void RemoveGames( IEnumerable<Game> games );

		void LoadGame( Game game );
	}
}
