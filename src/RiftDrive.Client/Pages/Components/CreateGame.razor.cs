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
using Microsoft.AspNetCore.Components.Web;
using RiftDrive.Client.Action;

namespace RiftDrive.Client.Pages.Components {
	public class CreateGameComponent : ComponentBase {

		public CreateGameComponent() {
			Dispatch = NullDispatch.Instance;
			UriHelper = NullUriHelper.Instance;

			GameName = "";
			PlayerName = "";
			CreatingGame = false;
		}

		[Inject] protected IDispatch Dispatch { get; set; }

		[Inject] protected NavigationManager UriHelper { get; set; }

		protected string GameName { get; set; }

		protected string PlayerName { get; set; }

		protected bool CreatingGame { get; set; }

		public async Task CreateGameClicked( MouseEventArgs args ) {
			string gameName = GameName;
			string playerName = PlayerName;
			await Dispatch.CreateGame( gameName, playerName );
			GameName = "";
			PlayerName = "";
			CreatingGame = false;
		}

		protected void ShowCreateClicked( MouseEventArgs args ) {
			CreatingGame = true;
		}
	}
}
