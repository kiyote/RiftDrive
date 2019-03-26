using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using RiftDrive.Client.Model;
using RiftDrive.Client.Services;

namespace RiftDrive.Client.Pages.Components.UserGames
{
    public class UserGamesComponent : ComponentBase
    {
		[Parameter] protected string Name { get; set; }

		[Inject] private IGameApiService _gameService { get; set; }

		protected Modal ModalRef { get; set; }

		protected IEnumerable<Game> Games { get; set; } = new List<Game>();

		public string GameName { get; set; }

		protected override async Task OnInitAsync() {
			Games = await _gameService.GetGames();
		}

		public async Task CreateGame() {
			ModalRef.Hide();
			await _gameService.CreateGame( GameName );
			GameName = default;
			Games = await _gameService.GetGames();
		}

		public async Task ShowModal() {
			ModalRef.Show();
		}

		public async Task CancelCreate() {
			ModalRef.Hide();
		}

		public void CancelCreate2() {
			ModalRef.Hide();
			Console.WriteLine( "Hide Modal 2" );
		}
	}
}
