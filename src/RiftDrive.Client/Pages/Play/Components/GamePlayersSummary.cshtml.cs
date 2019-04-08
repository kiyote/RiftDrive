using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using RiftDrive.Client.Model;
using RiftDrive.Client.Service;
using RiftDrive.Shared;

namespace RiftDrive.Client.Pages.Play.Components {
	public class GamePlayersSummaryComponent : ComponentBase {

		public GamePlayersSummaryComponent() {
			EditPlayerName = "";
			Players = new List<Player>();
		}

#nullable disable
		[Parameter] protected string GameId { get; set; }

		[Parameter] protected string GameName { get; set; }

		[Inject] protected IGameApiService GameService { get; set; }

		protected Modal ModalRef { get; set; }
#nullable enable

		protected IEnumerable<Player> Players { get; set; }

		protected string EditPlayerName { get; set; }

		protected override async Task OnParametersSetAsync() {
			if( !string.IsNullOrWhiteSpace( GameId ) ) {
				Players = await GameService.GetPlayers( new Id<Game>( GameId ) );
			}
		}

		public Task EditPlayer( Player player ) {
			ModalRef.Show();
			return Task.CompletedTask;
		}

		public Task CancelEdit() {
			ModalRef.Hide();
			return Task.CompletedTask;
		}

		public Task ApplyChanges() {
			ModalRef.Hide();
			return Task.CompletedTask;
		}
	}
}
