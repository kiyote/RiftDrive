using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RiftDrive.Client.Actions;

namespace RiftDrive.Client.Shared {
	public class MainLayoutBase: LayoutComponentBase, IDisposable {

		[Inject] public Config Config { get; set; }

		[Inject] public IDispatch Dispatch { get; set; }

		[Inject] public IIdentificationState State { get; set; }

		[Inject] public IStateMonitor StateMonitor { get; set; }

		protected override async Task OnInitializedAsync() {
			StateMonitor.OnStateChanged += AppState_OnStateChanged;
			await Dispatch.InitializeAsync();
		}

		private void AppState_OnStateChanged( object sender, EventArgs e ) {
			StateHasChanged();
		}

		public void Dispose() {
			StateMonitor.OnStateChanged -= AppState_OnStateChanged;
		}

		public string LoginUrl {
			get {
				return $"{Config.LogInUrl}&redirect_uri={Config.RedirectUrl}"; 
			}
		}
	}
}
