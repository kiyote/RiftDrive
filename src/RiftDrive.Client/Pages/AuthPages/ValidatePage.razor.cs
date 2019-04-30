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
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RiftDrive.Client.Model;
using RiftDrive.Client.Service;
using RiftDrive.Client.State;
using RiftDrive.Shared.Model;

namespace RiftDrive.Client.Pages.AuthPages {
	public class ValidatePageBase : ComponentBase, IDisposable {

		public static string Url = "/auth/validate";

		[Inject] protected IUriHelper UriHelper { get; set; }

		[Inject] protected IAppState State { get; set; }

		[Inject] protected ITokenService TokenService { get; set; }

		[Inject] protected IUserApiService UserService { get; set; }

		protected List<string> Messages { get; set; }

		protected int Progress { get; set; }

		public ValidatePageBase() {
			Messages = new List<string>();
		}

		protected override async Task OnInitAsync() {
			State.OnStateChanged += AppStateHasChanged;
			await State.Initialize();

			string code = UriHelper.GetParameter( "code" );
			Messages.Add( "...retrieving token..." );
			AuthorizationToken tokens = await TokenService.GetToken( code );
			if( tokens == default ) {
				//TODO: Do something here
				throw new InvalidOperationException();
			}
			await State.Update( State.Authentication, tokens.access_token, tokens.refresh_token, DateTime.UtcNow.AddSeconds( tokens.expires_in ) );

			Update( "...recording login...", 50 );
			await UserService.RecordLogin();

			Update( "...loading user information...", 75 );
			ClientUser userInfo = await UserService.GetUserInformation();
			await State.Update( State.Authentication, userInfo );

			UriHelper.NavigateTo( IndexPageBase.Url );
		}

		private void AppStateHasChanged( object sender, EventArgs e ) {
			StateHasChanged();
		}

		public void Dispose() {
			State.OnStateChanged -= AppStateHasChanged;
		}

		private void Update(string message, int progress) {
			Messages.Add( message );
			Progress = progress;
			StateHasChanged();
		}
	}
}
