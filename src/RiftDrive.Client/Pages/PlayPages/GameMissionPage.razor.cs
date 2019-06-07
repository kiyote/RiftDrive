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
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RiftDrive.Client.Action;
using RiftDrive.Client.State;
using RiftDrive.Shared.Model;

#nullable enable

namespace RiftDrive.Client.Pages.PlayPages {
	public class GameMissionPageBase: ComponentBase {
		public const string Url = "/game/{GameId}/mission";

		public GameMissionPageBase() {
			State = NullAppState.Instance;
			Dispatch = NullDispatch.Instance;
			GameId = "";
		}

		[Inject] protected IAppState State { get; set; }

		[Inject] protected IDispatch Dispatch { get; set; }

		[Parameter] protected string GameId { get; set; }

		public static string GetUrl( Id<Game> gameId ) {
			return $"game/{gameId.Value}/mission";
		}

		public void Dispose() {
			State.OnStateChanged -= OnStateHasChanged;
		}

		protected override async Task OnInitAsync() {
			State.OnStateChanged += OnStateHasChanged;

			Id<Game> gameId = new Id<Game>( GameId );
			await Dispatch.LoadCurrentMission( gameId );
		}

		private void OnStateHasChanged( object sender, EventArgs args ) {
			StateHasChanged();
		}
	}
}
