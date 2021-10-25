using System;
using RiftDrive.Common.Model;

namespace RiftDrive.Common.Messages {
	public sealed class GameStateUpdate {

		public GameStateUpdate(
			Game game,
			GameState state
		) {
			Game = game;
			State = state;
		}

		public GameStateUpdate() {
		}

		public Game Game { get; set; }

		public GameState State { get; set; }

	}
}
