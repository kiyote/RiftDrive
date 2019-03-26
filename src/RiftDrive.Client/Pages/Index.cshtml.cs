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

namespace RiftDrive.Client.Pages {
	public class IndexComponent: ComponentBase {

		public static string Url = "/";

		[Inject] private IAppState _appState { get; set; }

		protected string Name { get; set; }

		protected bool IsAuthenticated { get; set; }

		protected override async Task OnInitAsync() {
			Name = await _appState.GetName();
			IsAuthenticated = await _appState.GetIsAuthenticated();
		}
	}
}
