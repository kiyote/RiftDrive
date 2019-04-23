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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using RiftDrive.Client.Service;
using RiftDrive.Client.State;
using RiftDrive.Shared;

namespace RiftDrive.Client.Pages.Play {
	public class GameViewComponent : ComponentBase
    {
		public const string Url = "/game/view";

#nullable disable
		[Inject] protected IAppState State { get; set; }

		[Inject] protected IUriHelper UriHelper { get; set; }

		[Inject] protected IGameApiService GameService { get; set; }
#nullable enable

		protected override async Task OnInitAsync() {
			/*
			string gameIdValue = await State.GetPlayGameId();
			if( string.IsNullOrWhiteSpace( gameIdValue ) ) {
				UriHelper.NavigateTo( IndexComponent.Url );
			}
			Game = await GameService.GetGame( new Id<Game>( gameIdValue ) );

			// If we didn't find a game, don't continue
			if (Game == default) {
				UriHelper.NavigateTo( IndexComponent.Url );
			}
			*/
		}
	}
}
