using System;
using System.Collections.Generic;
using System.Text;

namespace RiftDrive.Client.State {
	public interface IValidationState {
		IEnumerable<string> Messages { get; }
		int Progress { get; }
	}
}
