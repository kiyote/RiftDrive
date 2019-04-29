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
using RiftDrive.Client.Model;
using RiftDrive.Client.Service;
using RiftDrive.Client.State;

namespace RiftDrive.Client.Pages.AuthPages {
	public class ValidatePageBase : ComponentBase {

		public static string Url = "/auth/validate";

		[Inject] protected IUriHelper UriHelper { get; set; }

		[Inject] protected IAppState State { get; set; }

		[Inject] protected IDispatch Dispatch { get; set; }

		protected override async Task OnInitAsync() {
			State.OnStateChanged += AppStateHasChanged;
			await State.Initialize();

			string code = UriHelper.GetParameter( "code" );
			await Dispatch.RetrieveTokens( code );
			await Dispatch.LogInUser();

			State.OnStateChanged -= AppStateHasChanged;
			UriHelper.NavigateTo( IndexPageBase.Url );
		}

		private void AppStateHasChanged( object sender, EventArgs e ) {
			StateHasChanged();
		}
	}
}
