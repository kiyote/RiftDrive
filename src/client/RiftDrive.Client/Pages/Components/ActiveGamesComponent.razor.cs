using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RiftDrive.Client.Actions;
using RiftDrive.Client.Pages.Play;
using RiftDrive.Common.Model;

namespace RiftDrive.Client.Pages.Components {
	public class ActiveGamesComponentBase: ComponentBase {

		[Parameter] public IEnumerable<Game> Games { get; set; }

		[Parameter] public User User { get; set; }

		[Inject] public IGameManagementDispatch Dispatch { get; set; }

		[Inject] public NavigationManager Navigator { get; set; }

		public void PlayGame( Id<Game> gameId ) {
			Navigator.NavigateTo( GameViewPageBase.GetUrl( gameId ) );
		}

		public async Task DeleteGame( Id<Game> gameId ) {
			await Dispatch.DeleteGameAsync( gameId );
		}

		protected override bool ShouldRender() {
			return true;
		}
	}
}
