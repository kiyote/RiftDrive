using System;

namespace RiftDrive.Common.Messages {
	public class CreateGameRequest {

		public CreateGameRequest(
			string gameName,
			string playerName
		) {
			GameName = gameName;
			PlayerName = playerName;
		}

		public CreateGameRequest() {
		}

		public string GameName { get; set; }

		public string PlayerName { get; set; }
	}
}
