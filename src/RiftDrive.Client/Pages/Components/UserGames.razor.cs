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
using RiftDrive.Client.Action;
using RiftDrive.Client.Pages.PlayPages;
using RiftDrive.Shared.Model;

namespace RiftDrive.Client.Pages.Components {
	public class UserGamesComponent : ComponentBase {

#nullable disable
		[Inject] protected IDispatch Dispatch { get; set; }

		[Inject] protected IUriHelper UriHelper { get; set; }

		[Parameter] protected IEnumerable<Game> Games { get; set; }

		[Parameter] protected ClientUser User { get; set; }
#nullable enable

		public void PlayGame( Id<Game> gameId ) {
			UriHelper.NavigateTo( GameViewPageBase.GetUrl( gameId ) );
		}

		public async Task DeleteGame( Id<Game> gameId ) {
			await Dispatch.DeleteGame( gameId );
		}
	}
}
