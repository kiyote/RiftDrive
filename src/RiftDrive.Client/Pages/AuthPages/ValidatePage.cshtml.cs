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
using Microsoft.AspNetCore.Components.Services;
using RiftDrive.Client.Model;
using RiftDrive.Client.Service;
using RiftDrive.Client.State;

namespace RiftDrive.Client.Pages.AuthPages {
	public class ValidatePageBase : ComponentBase {

		public static string Url = "/auth/validate";

#nullable disable
		[Inject] protected IUriHelper UriHelper { get; set; }

		[Inject] protected IUserApiService UserApiService { get; set; }

		[Inject] protected IAppState State { get; set; }

		[Inject] protected ITokenService TokenService { get; set; }
#nullable enable

		protected override async Task OnInitAsync() {
			State.OnStateChanged += AppStateHasChanged;
			await State.Initialize();

			string code = UriHelper.GetParameter( "code" );

			await State.UpdateValidationProgress( "...retrieving tokens...", 5 );
			AuthorizationToken tokens = await TokenService.GetToken( code );
			if( tokens == default ) {
				//TODO: Do something here
				throw new InvalidOperationException();
			}
			await State.SetTokens( tokens.access_token, tokens.refresh_token, DateTime.UtcNow.AddSeconds( tokens.expires_in ) );

			await State.UpdateValidationProgress( "...recording login...", 50 );
			await UserApiService.RecordLogin();

			await State.UpdateValidationProgress( "...retrieving user information...", 75 );
			Model.User userInfo = await UserApiService.GetUserInformation();

			await State.SetUserInformation( userInfo.Username, userInfo.Name );
			State.OnStateChanged -= AppStateHasChanged;
			UriHelper.NavigateTo( IndexPageBase.Url );
		}

		private void AppStateHasChanged( object sender, EventArgs e ) {
			StateHasChanged();
		}
	}
}
