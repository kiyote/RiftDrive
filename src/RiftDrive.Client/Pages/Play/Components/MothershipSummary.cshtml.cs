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
using RiftDrive.Shared;

namespace RiftDrive.Client.Pages.Play.Components {
	public class MothershipSummaryComponent : ComponentBase
    {
#nullable disable
		[Inject] protected IAppState State { get; set; }

		[Inject] protected IGameApiService GameService { get; set; }

		[Parameter] protected Mothership Mothership { get; set; }

		[Parameter] protected Game Game { get; set; }
#nullable enable

		protected override async Task OnInitAsync() {
			IEnumerable<MothershipAttachedModule> modules = await GameService.GetMothershipModules( Mothership.GameId, Mothership.Id );
			await State.SetMothershipModules( modules );
		}
	}
}
