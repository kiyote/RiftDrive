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

namespace RiftDrive.Client.Pages {
	public class MainLayoutComponent : LayoutComponentBase, IDisposable {

		[Inject] private IAppState _state { get; set; }

		public string Name { get; set; }

		public bool IsAuthenticated { get; set; }

		protected override void OnInit() {
			_state.OnStateChanged += AppState_OnStateChanged;
		}

		protected override async Task OnInitAsync() {
			Name = await _state.GetName();
			IsAuthenticated = await _state.GetIsAuthenticated();
		}

		private void AppState_OnStateChanged( object sender, EventArgs e ) {
			StateHasChanged();
		}

		public void Dispose() {
			_state.OnStateChanged -= AppState_OnStateChanged;
		}
	}
}
