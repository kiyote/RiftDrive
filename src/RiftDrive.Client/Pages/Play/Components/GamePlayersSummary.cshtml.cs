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

		[Parameter] protected string GameId { get; set; }

		[Parameter] protected string GameName { get; set; }

		[Inject] protected IGameApiService GameService { get; set; }

		protected IEnumerable<Player> Players { get; set; } = new List<Player>();

		protected Modal ModalRef { get; set; }

		protected string EditPlayerName { get; set; }

		protected override async Task OnParametersSetAsync() {
			if( !string.IsNullOrWhiteSpace( GameId ) ) {
				Players = await GameService.GetPlayers( new Id<Game>( GameId ) );
			}
		}

		public async Task EditPlayer( Player player ) {
			ModalRef.Show();
		}

		public async Task CancelEdit() {
			ModalRef.Hide();
		}

		public async Task ApplyChanges() {
			ModalRef.Hide();
		}
	}
}
