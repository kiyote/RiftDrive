using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using RiftDrive.Client.Model;
using RiftDrive.Client.Pages.Play;
using RiftDrive.Client.Services;
using RiftDrive.Shared;

namespace RiftDrive.Client.Pages.Components.UserGames
{
    public class UserGamesComponent : ComponentBase
    {
		[Parameter] protected string Name { get; set; }

		[Inject] private IGameApiService _gameService { get; set; }

		[Inject] private IUriHelper _uriHelper { get; set; }

		[Inject] private IAppState _state { get; set; }

		protected Modal ModalRef { get; set; }

		protected IEnumerable<Game> Games { get; set; } = new List<Game>();

		public string GameName { get; set; }

		public string PlayerName { get; set; }

		public bool Busy { get; set; }

		protected override async Task OnInitAsync() {
			Games = await _gameService.GetGames();
		}

		public async Task CreateGame() {
			ModalRef.Hide();
			Busy = true;
			await _gameService.CreateGame( GameName, PlayerName );
			GameName = default;
			PlayerName = default;
			Games = await _gameService.GetGames();
			Busy = false;
		}

		public async Task PlayGame(Id<Game> gameId) {
			await _state.SetPlayGameId( gameId.Value );
			_uriHelper.NavigateTo( GameSummaryComponent.Url );
		}

		public async Task ShowModal() {
			ModalRef.Show();
		}

		public async Task CancelCreate() {
			ModalRef.Hide();
		}

		public void CancelCreate2() {
			ModalRef.Hide();
		}
	}
}
