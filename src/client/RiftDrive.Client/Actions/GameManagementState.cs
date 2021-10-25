using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RiftDrive.Common.Model;

namespace RiftDrive.Client.Actions {
	internal sealed class GameManagementState : IGameManagementState, IGameManagementStateMutator {

		private List<Game> _availableGames;

		private List<Game> _activeGames;

		private ImmutableDictionary<Id<Game>, Game> _loadedGames;

		public GameManagementState() {
			_availableGames = new List<Game>();
			_activeGames = new List<Game>();
			_loadedGames = new Dictionary<Id<Game>, Game>().ToImmutableDictionary();
		}

		IEnumerable<Game> IGameManagementState.AvailableGames => _availableGames;

		IEnumerable<Game> IGameManagementState.ActiveGames => _activeGames;

		IImmutableDictionary<Id<Game>, Game> IGameManagementState.Games => _loadedGames;

		void IGameManagementStateMutator.AddAvailableGames( IEnumerable<Game> games ) {
			_availableGames = _availableGames.Union( games ).ToList();
		}

		void IGameManagementStateMutator.AddActiveGames( IEnumerable<Game> games ) {
			_activeGames = _activeGames.Union( games ).ToList();
		}

		void IGameManagementStateMutator.RemoveGames( IEnumerable<Game> games ) {
			_availableGames = _availableGames.Except( games ).ToList();
			_activeGames = _activeGames.Except( games ).ToList();
		}

		void IGameManagementStateMutator.LoadGame( Game game ) {
			_loadedGames = _loadedGames.Add( game.Id, game );
		}
	}
}
