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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RiftDrive.Client.Service;
using RiftDrive.Client.State;
using RiftDrive.Shared.Model;

namespace RiftDrive.Client.Pages.PlayPages {
	public class GameViewPageBase : ComponentBase {
		public const string Url = "/game/view";

		[Inject] protected IAppState State { get; set; }

		[Inject] protected IGameApiService GameService { get; set; }

		[Parameter] protected string GameId { get; set; }

		protected Game Game { get; set; }

		protected Mothership Mothership { get; set; }

		protected IEnumerable<Actor> Crew { get; set; }

		protected IEnumerable<MothershipAttachedModule> Modules { get; set; }

		public GameViewPageBase() {
			Crew = new List<Actor>();
		}

		public static string GetUrl( Id<Game> gameId ) {
			return $"{Url}/{gameId.Value}";
		}

		protected override async Task OnInitAsync() {
			var gameId = new Id<Game>( GameId );
			Game = await GameService.GetGame( gameId );
			Mothership = await GameService.GetMothership( gameId );
			Crew = await GameService.GetCrew( gameId );
			Modules = await GameService.GetMothershipModules( gameId, Mothership.Id );
		}
	}
}