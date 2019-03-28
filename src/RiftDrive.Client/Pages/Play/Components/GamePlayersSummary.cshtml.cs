using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using RiftDrive.Client.Model;
using RiftDrive.Client.Services;
using RiftDrive.Shared;

namespace RiftDrive.Client.Pages.Play.Components
{
    public class GamePlayersSummaryComponent : ComponentBase
    {

		protected IEnumerable<Player> Players { get; set; } = new List<Player>();

		protected Modal ModalRef { get; set; }

		protected string EditPlayerName { get; set; }

		[Parameter] protected string GameId { get; set; }

		[Parameter] protected string GameName { get; set; }

		[Inject] private IGameApiService _gameService { get; set; }

		protected override async Task OnParametersSetAsync() {
			if( !string.IsNullOrWhiteSpace( GameId ) ) {
				Players = await _gameService.GetPlayers( new Id<Game>( GameId ) );
				Console.WriteLine( $"Set Players : {Players.Count()}" );
			}
		}

		public async Task EditPlayer(Player playerId) {
			ModalRef.Show();
		}

		public void CancelEdit2() {
			ModalRef.Hide();
		}

		public async Task CancelEdit() {
			ModalRef.Hide();
		}

		public async Task ApplyChanges() {
			ModalRef.Hide();
		}
    }
}
