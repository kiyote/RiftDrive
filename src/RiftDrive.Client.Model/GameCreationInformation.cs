using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RiftDrive.Client.Model {
	public sealed class GameCreationInformation {

		[JsonConstructor]
		public GameCreationInformation(
			string gameName,
			string playerName
		) {
			GameName = gameName;
			PlayerName = playerName;
		}

		public string GameName { get; }

		public string PlayerName { get; }
	}
}
