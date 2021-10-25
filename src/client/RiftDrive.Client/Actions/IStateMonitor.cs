using System;

namespace RiftDrive.Client.Actions {
	public interface IStateMonitor {

		event EventHandler OnStateChanged;

		void FireOnStateChanged();
	}
}
