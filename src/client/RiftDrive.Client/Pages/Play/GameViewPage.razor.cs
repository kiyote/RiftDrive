using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RiftDrive.Client.Actions;
using RiftDrive.Common.Model;

namespace RiftDrive.Client.Pages.Play
{
	public class GameViewPageBase : ComponentBase, IDisposable {
		public const string Url = "/game/{0}/view";

		[Parameter] public string GameIdValue { get; set; }

		[Inject] public IStateMonitor StateMonitor { get; set; }

		[Inject] public IGameManagementState State { get; set; }

		[Inject] public IGameManagementDispatch Dispatch { get; set; }

		public Id<Game> GameId { get; set; }

		public static string GetUrl( Id<Game> gameId ) {
			return string.Format( Url, gameId.Value );
		}

		public void Dispose() {
			StateMonitor.OnStateChanged -= AppState_OnStateChanged;
		}

		protected override async Task OnInitializedAsync() {
			StateMonitor.OnStateChanged += AppState_OnStateChanged;

			if( !string.IsNullOrWhiteSpace( GameIdValue )) {
				GameId = new Id<Game>( GameIdValue );
				await Dispatch.LoadGameAsync( GameId );
			}
		}

		private void AppState_OnStateChanged( object sender, EventArgs e ) {
			StateHasChanged();
		}

	}
}
