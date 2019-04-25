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
using RiftDrive.Client.Action;
using RiftDrive.Client.State;

namespace RiftDrive.Client.Pages.PlayPages {
	public class GameSummaryPageBase : ComponentBase {

		public const string Url = "/game/summary";

#nullable disable
		[Inject] protected IAppState State { get; set; }

		[Inject] protected IUriHelper UriHelper { get; set; }

		[Inject] protected IDispatch Dispatch { get; set; }
#nullable enable

		protected async Task StartGame() {
			await Dispatch.StartGame( State.GamePlay.Game.Id, "Game has started!" );
			UriHelper.NavigateTo( GameViewPageBase.Url );
		}

		protected async Task PlayGame() {
			await Dispatch.PlayGame( State.GamePlay.Game.Id );
			UriHelper.NavigateTo( GameViewPageBase.Url );
		}
	}
}
