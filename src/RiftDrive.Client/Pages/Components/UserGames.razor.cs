/*
 * Copyright 2018-2019 Todd Lang
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using RiftDrive.Client.Pages.PlayPages;
using RiftDrive.Client.Service;
using RiftDrive.Shared.Model;

namespace RiftDrive.Client.Pages.Components {
	public class UserGamesComponent : ComponentBase {

		[Inject] protected IGameApiService GameService { get; set; }

		[Inject] protected IUriHelper UriHelper { get; set; }

		[Parameter] protected ClientUser User { get; set; }

		protected List<Game> Games { get; set; }

		protected string GameName { get; set; }

		protected string PlayerName { get; set; }

		protected bool Busy { get; set; }

		protected Modal ModalRef { get; set; }

		public UserGamesComponent() {
			Games = new List<Game>();
		}

		protected override async Task OnInitAsync() {
			IEnumerable<Game> games = await GameService.GetGames();
			Games = games.ToList();
		}

		public async Task CreateGame() {
			ModalRef.Hide();
			Busy = true;

			Game newGame = await GameService.CreateGame( GameName, PlayerName );
			Games.Add( newGame );

			GameName = "";
			PlayerName = "";
			Busy = false;
		}

		public void PlayGame( Id<Game> gameId ) {
			UriHelper.NavigateTo( GameViewPageBase.GetUrl(gameId) );
		}

		public async Task DeleteGame( Id<Game> gameId ) {
			await GameService.DeleteGame( gameId );
			IEnumerable<Game> games = await GameService.GetGames();
			Games = games.ToList();
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
