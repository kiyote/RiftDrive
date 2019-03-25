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
using RiftDrive.Client.Services;

namespace RiftDrive.Client.Pages.Auth {
	public class ValidateComponent : ComponentBase {

		public static string Url = "/auth/validate";

		[Inject] private IUriHelper _uriHelper { get; set; }

		[Inject] private IAccessTokenProvider _accessTokenProvider { get; set; }

		[Inject] private IUserApiService _userApiService { get; set; }

		[Inject] private IAppState _state { get; set; }

		[Inject] private ITokenService _tokenService { get; set; }

		protected List<string> Messages { get; set; } = new List<string>();

		protected int Progress { get; set; }

		protected override async Task OnInitAsync() {
			var code = _uriHelper.GetParameter( "code" );

			Messages.Add( "...retrieving tokens..." );
			StateHasChanged();
			var tokens = await _tokenService.GetToken( code );
			await _accessTokenProvider.SetTokens( tokens.access_token, tokens.refresh_token, DateTime.UtcNow.AddSeconds( tokens.expires_in ) );

			Progress = 50;
			Messages.Add( "...recording login..." );
			StateHasChanged();
			await _userApiService.RecordLogin();

			Progress = 75;
			Messages.Add( "...retrieving user information..." );
			StateHasChanged();
			var userInfo = await _userApiService.GetUserInformation();

			await _state.SetUsername( userInfo.Username );
			await _state.SetName( userInfo.Name );
			_uriHelper.NavigateTo( IndexComponent.Url );
		}
	}
}
