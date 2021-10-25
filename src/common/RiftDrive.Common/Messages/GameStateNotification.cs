using System;
using System.Linq;
using System.Collections.Generic;

namespace RiftDrive.Common.Messages {
	public class GameStateNotification {

		public GameStateNotification(
			IEnumerable<GameStateUpdate> updates
		) {
			Updates = updates.ToList(); ;
		}

		public GameStateNotification() {
		}

		public IEnumerable<GameStateUpdate> Updates { get; set; }
	}
}
