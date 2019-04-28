using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RiftDrive.Client.State {
	internal sealed class ValidationState : IValidationState {

		public ValidationState() {
			Messages = new List<string>();
			Progress = 0;
		}

		[JsonConstructor]
		public ValidationState(
			IEnumerable<string> messages,
			int progress
		) {
			Messages = messages;
			Progress = progress;
		}

		public IEnumerable<string> Messages { get; }

		public int Progress { get; }
	}
}
