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
using Microsoft.AspNetCore.Components.Services;
using RiftDrive.Client.Model;
using RiftDrive.Client.Service;

namespace RiftDrive.Client.Pages.Auth {
	public class ValidateComponent : ComponentBase {

		public static string Url = "/auth/validate";

#nullable disable
		[Inject] protected IUriHelper UriHelper { get; set; }

		[Inject] protected IAccessTokenProvider AccessTokenProvider { get; set; }

		[Inject] protected IUserApiService UserApiService { get; set; }

		[Inject] protected IAppState State { get; set; }

		[Inject] protected ITokenService TokenService { get; set; }
#nullable enable

		protected List<string> Messages { get; set; } = new List<string>();

		protected int Progress { get; set; }

		protected override async Task OnInitAsync() {
			string code = UriHelper.GetParameter( "code" );

			Messages.Add( "...retrieving tokens..." );
			StateHasChanged();
			AuthorizationToken? tokens = await TokenService.GetToken( code );
			if (tokens == default) {
				//TODO: Do something here
				throw new InvalidOperationException();
			}
			await AccessTokenProvider.SetTokens( tokens.access_token, tokens.refresh_token, DateTime.UtcNow.AddSeconds( tokens.expires_in ) );

			Progress = 50;
			Messages.Add( "...recording login..." );
			StateHasChanged();
			await UserApiService.RecordLogin();

			Progress = 75;
			Messages.Add( "...retrieving user information..." );
			StateHasChanged();
			Model.User userInfo = await UserApiService.GetUserInformation();

			await State.SetUsername( userInfo.Username );
			await State.SetName( userInfo.Name );
			UriHelper.NavigateTo( IndexComponent.Url );
		}
	}
}
