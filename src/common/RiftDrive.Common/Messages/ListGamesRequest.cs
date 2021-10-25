using System;
using System.Collections.Generic;
using System.Text;
using RiftDrive.Common.Model;

namespace RiftDrive.Common.Messages {
	public sealed class ListGamesRequest {

		public enum ListGameType {
			Unknown,
			All,
			Available,
			Active
		}

		public ListGamesRequest(
			Id<User> userId,
			ListGameType gameType
		) {
			GameType = gameType;
			UserId = userId;
		}

		public ListGameType GameType { get; }

		public Id<User> UserId { get; }
	}
}
