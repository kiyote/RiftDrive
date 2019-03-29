using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using Microsoft.JSInterop;
using RiftDrive.Client.Model;
using RiftDrive.Client.Services;
using RiftDrive.Shared;

namespace RiftDrive.Client.Pages.Play {
	public class GameSummaryComponent : ComponentBase {

		public const string Url = "/game/summary";

		[Inject] private IAppState _state { get; set; }

		[Inject] private IUriHelper _uriHelper { get; set; }

		[Inject] private IGameApiService _gameService { get; set; }

		protected Game Game { get; set; }

		protected override async Task OnInitAsync() {
			var gameIdValue = await _state.GetPlayGameId();
			if( string.IsNullOrWhiteSpace(gameIdValue)) {
				_uriHelper.NavigateTo( IndexComponent.Url );
			}
			Game = await _gameService.GetGame( new Id<Game>( gameIdValue ) );
		}

		protected async Task StartGame() {

		}

		protected async Task PlayGame() {

		}
	}
}
