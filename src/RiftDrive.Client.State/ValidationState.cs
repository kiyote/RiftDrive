using System;
using System.Collections.Generic;
using System.Text;

namespace RiftDrive.Client.State {
	internal sealed class ValidationState : IValidationState {

		public ValidationState() {
			Messages = new List<string>();
			Progress = 0;
		}

		public ValidationState(
			IValidationState initial,
			string message,
			int progress
		) {
			var messages = new List<string>( initial.Messages );
			messages.Add( message );
			Messages = messages;
			Progress = progress;
		}

		public IEnumerable<string> Messages { get; }

		public int Progress { get; }
	}
}
