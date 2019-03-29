using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RiftDrive.Shared {
	[JsonConverter( typeof( StringEnumConverter ) )]
	public enum GameState {
		Unknown = -1,

		WaitingForPlayers = 0,

		Active = 1
	}
}
