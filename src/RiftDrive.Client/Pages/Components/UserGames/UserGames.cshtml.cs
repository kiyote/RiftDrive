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

#nullable disable
		[Parameter] protected string Name { get; set; }

		[Inject] protected IGameApiService GameService { get; set; }

		[Inject] protected IUriHelper UriHelper { get; set; }

		[Inject] protected IAppState State { get; set; }

		protected Modal ModalRef { get; set; }
#nullable enable

		protected IEnumerable<Game> Games { get; set; } = new List<Game>();

		public string GameName { get; set; }

		public string PlayerName { get; set; }

		public bool Busy { get; set; }

		public UserGamesComponent() {
			GameName = "";
			PlayerName = "";
		}

		protected override async Task OnInitAsync() {
			Games = await GameService.GetGames();
		}

		public async Task CreateGame() {
			ModalRef.Hide();
			Busy = true;
			await GameService.CreateGame( GameName, PlayerName );
			GameName = "";
			PlayerName = "";
			Games = await GameService.GetGames();
			Busy = false;
		}

		public async Task PlayGame( Id<Game> gameId ) {
			await State.SetPlayGameId( gameId.Value );
			UriHelper.NavigateTo( GameSummaryComponent.Url );
		}

		public Task ShowModal() {
			ModalRef.Show();
			return Task.CompletedTask;
		}

		public Task CancelCreate() {
			ModalRef.Hide();
			return Task.CompletedTask;
		}
	}
}
