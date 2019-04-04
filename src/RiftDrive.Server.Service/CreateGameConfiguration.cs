using System;
using System.Collections.Generic;
using System.Text;
using RiftDrive.Server.Model;
using RiftDrive.Shared;

namespace RiftDrive.Server.Service {
	public sealed class CreateGameConfiguration {

		public CreateGameConfiguration(
			Id<User> createdBy,
			DateTime createdOn,
			string gameName,
			string playerName
		) {
			CreatedBy = createdBy;
			CreatedOn = createdOn.ToUniversalTime();
			GameName = gameName;
			PlayerName = playerName;
		}

		public Id<User> CreatedBy { get; }

		public DateTime CreatedOn { get; }

		public string GameName { get; }

		public string PlayerName { get; }
	}
}
