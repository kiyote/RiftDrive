using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using RiftDrive.Client.Model;
using RiftDrive.Client.Service;
using RiftDrive.Client.State;
using RiftDrive.Shared;

namespace RiftDrive.Client.Pages.Play
{
    public class GameViewComponent : ComponentBase
    {
		public const string Url = "/game/view";

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

			// If we didn't find a game, don't continue
			if (Game == default) {
				UriHelper.NavigateTo( IndexComponent.Url );
			}
		}
	}
}
