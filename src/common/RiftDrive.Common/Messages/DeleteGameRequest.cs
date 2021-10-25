using System;
using System.Collections.Generic;
using System.Text;
using RiftDrive.Common.Model;

namespace RiftDrive.Common.Messages {
	public sealed class DeleteGameRequest {

		public DeleteGameRequest(
			Id<Game> gameId
		) {
			GameId = gameId;
		}

		public DeleteGameRequest() {
		}

		public Id<Game> GameId { get; set; }
	}
}
