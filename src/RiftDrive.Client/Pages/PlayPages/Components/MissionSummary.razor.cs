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
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using RiftDrive.Shared.Model;

namespace RiftDrive.Client.Pages.PlayPages.Components {
	public class MissionSummaryComponent : ComponentBase {

		public MissionSummaryComponent() {
			UriHelper = NullUriHelper.Instance;
		}

		[Parameter] public Mission? Mission { get; set; }

		[Inject] protected NavigationManager UriHelper { get; set; }

		protected void ResumeClicked( MouseEventArgs args ) {
			if (Mission != default) {
				UriHelper.NavigateTo( GameMissionPageBase.GetUrl( Mission.GameId ) );
			}
		}
	}
}
