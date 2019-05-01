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
using RiftDrive.Client.Service;
using RiftDrive.Shared.Model;

namespace RiftDrive.Client.Pages.PlayPages.Components {
	public class MothershipModuleSummaryComponent : ComponentBase {

		public MothershipModuleSummaryComponent() {
			Modules = new List<MothershipAttachedModule>();
		}

		[Parameter] protected IEnumerable<MothershipAttachedModule> Modules { get; set; }

		[Parameter] protected Game Game { get; set; }

		[Inject] protected IDispatch Dispatch { get; set; }

		protected async Task ModuleButtonClicked(
			MothershipAttachedModule module,
			MothershipModuleAction action
		) {
			MothershipModule definition = MothershipModule.GetById( module.MothershipModuleId );
			await Dispatch.TriggerModuleAction( Game.Id, module.MothershipId, module.MothershipModuleId, action.Id );
		}
	}
}
