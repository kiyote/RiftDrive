using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using RiftDrive.Client.Model;
using RiftDrive.Client.Pages.Play;
using RiftDrive.Client.Service;
using RiftDrive.Shared;

namespace RiftDrive.Client.Pages.Components.UserGames {
	public class UserGamesComponent : ComponentBase {
		[Parameter] protected string Name { get; set; }

		[Inject] protected IGameApiService GameService { get; set; }

		[Inject] protected IUriHelper UriHelper { get; set; }

		[Inject] protected IAppState State { get; set; }

		protected Modal ModalRef { get; set; }

		protected IEnumerable<Game> Games { get; set; } = new List<Game>();

		public string GameName { get; set; }

		public string PlayerName { get; set; }

		public bool Busy { get; set; }

		protected override async Task OnInitAsync() {
			Games = await GameService.GetGames();
		}

		public async Task CreateGame() {
			ModalRef.Hide();
			Busy = true;
			await GameService.CreateGame( GameName, PlayerName );
			GameName = default;
			PlayerName = default;
			Games = await GameService.GetGames();
			Busy = false;
		}

		public async Task PlayGame( Id<Game> gameId ) {
			await State.SetPlayGameId( gameId.Value );
			UriHelper.NavigateTo( GameSummaryComponent.Url );
		}

		public async Task ShowModal() {
			ModalRef.Show();
		}

		public async Task CancelCreate() {
			ModalRef.Hide();
		}
	}
}
