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
		[Inject] protected IGameApiService GameService { get; set; }

		protected Modal ModalRef { get; set; }
#nullable enable

		[Parameter] protected Game? Game { get; set; }

		protected IEnumerable<Player> Players { get; set; }

		protected string EditPlayerName { get; set; }

		protected override async Task OnParametersSetAsync() {
			if( Game != null ) {
				Players = await GameService.GetPlayers( Game.Id );
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
