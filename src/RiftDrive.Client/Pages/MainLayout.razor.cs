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
using Microsoft.AspNetCore.Components.Layouts;
using RiftDrive.Client.State;

namespace RiftDrive.Client.Pages {
	public class MainLayoutBase : LayoutComponentBase, IDisposable {

#nullable disable
		[Inject] protected IAppState State { get; set; }
#nullable enable

		protected override async Task OnInitAsync() {
			State.OnStateChanged += AppState_OnStateChanged;
			await State.Initialize();
		}

		private void AppState_OnStateChanged( object sender, EventArgs e ) {
			StateHasChanged();
		}

		public void Dispose() {
			State.OnStateChanged -= AppState_OnStateChanged;
		}
	}
}
