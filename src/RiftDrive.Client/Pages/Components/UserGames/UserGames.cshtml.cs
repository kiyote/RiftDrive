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
using Microsoft.AspNetCore.Components.Services;
using RiftDrive.Client.Pages.Play;
using RiftDrive.Client.Service;
using RiftDrive.Client.State;
using RiftDrive.Shared;

namespace RiftDrive.Client.Pages.Components.UserGames {
	public class UserGamesComponent : ComponentBase {

#nullable disable
		[Inject] protected IGameApiService GameService { get; set; }

		[Inject] protected IUriHelper UriHelper { get; set; }

		[Parameter] protected IAuthenticationState AuthenticationState { get; set; }

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
			await State.SetGame( Games.First( g => g.Id.Equals( gameId )));
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
