using System;
using RiftDrive.Common.Model;

namespace RiftDrive.Common.Messages {
	public class GameUpdateNotification {

		public GameUpdateNotification(
			Game game
		) {
			Game = game;
		}

		public GameUpdateNotification() {
		}

		public Game Game { get; set; }
	}
}
