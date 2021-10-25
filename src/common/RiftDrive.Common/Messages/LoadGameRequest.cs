using System;
using RiftDrive.Common.Model;

namespace RiftDrive.Common.Messages {
	public class LoadGameRequest {

		public LoadGameRequest(
			Id<Game> gameId
		) {
			GameId = gameId;
		}

		public LoadGameRequest() {
		}

		public Id<Game> GameId { get; set; }
	}
}
