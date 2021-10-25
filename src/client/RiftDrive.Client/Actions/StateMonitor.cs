using System;

namespace RiftDrive.Client.Actions {
	internal sealed class StateMonitor : IStateMonitor {

		public event EventHandler OnStateChanged;

		void IStateMonitor.FireOnStateChanged() {
			OnStateChanged?.Invoke( this, EventArgs.Empty );
		}
	}
}
