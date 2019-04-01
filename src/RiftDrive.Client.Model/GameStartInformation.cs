using System;
using System.Collections.Generic;
using System.Text;

namespace RiftDrive.Client.Model {
	public sealed class GameStartInformation {
		public GameStartInformation(
			string message
		) {
			Message = message;
		}

		public string Message { get; }
	}
}
