using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using RiftDrive.Client.Model;
using RiftDrive.Client.Service;
using RiftDrive.Shared;

namespace RiftDrive.Client.Pages.Play {
	public class GameSummaryComponent : ComponentBase {

		public const string Url = "/game/summary";

#nullable disable
		[Inject] protected IAppState State { get; set; }

		[Inject] protected IUriHelper UriHelper { get; set; }

		[Inject] protected IGameApiService GameService { get; set; }
#nullable enable
		protected Game? Game { get; set; }

		protected override async Task OnInitAsync() {
			string gameIdValue = await State.GetPlayGameId();
			if( string.IsNullOrWhiteSpace( gameIdValue ) ) {
				UriHelper.NavigateTo( IndexComponent.Url );
			}
			Game = await GameService.GetGame( new Id<Game>( gameIdValue ) );

			if (Game == default) {
				UriHelper.NavigateTo( IndexComponent.Url );
			}
		}

		protected async Task StartGame() {
			await GameService.StartGame( Game!.Id, "Game has started!" );
			UriHelper.NavigateTo( GameViewComponent.Url );
		}

		protected Task PlayGame() {
			UriHelper.NavigateTo( GameViewComponent.Url );
			return Task.CompletedTask;
		}
	}
}
