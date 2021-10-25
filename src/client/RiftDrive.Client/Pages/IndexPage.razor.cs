using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using RiftDrive.Client.Actions;

namespace RiftDrive.Client.Pages {
	public class IndexPageBase: ComponentBase, IDisposable {

		public const string Url = "/";

		[Inject] public IIdentificationState IdentificationState { get; set; }

		[Inject] public IGameManagementState GameManagementState { get; set; }

		[Inject] public IStateMonitor StateMonitor { get; set; }

		protected override void OnInitialized() {
			StateMonitor.OnStateChanged += AppState_OnStateChanged;
		}

		private void AppState_OnStateChanged( object sender, EventArgs e ) {
			StateHasChanged();
		}

		public void Dispose() {
			StateMonitor.OnStateChanged -= AppState_OnStateChanged;
		}
	}
}
