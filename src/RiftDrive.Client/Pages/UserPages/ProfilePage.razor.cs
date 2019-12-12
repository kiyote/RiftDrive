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
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RiftDrive.Client.Action;
using RiftDrive.Client.State;
using RiftDrive.Shared.Model;
using RiftDrive.Shared.Model.Client;

namespace RiftDrive.Client.Pages.UserPages {
	public class ProfilePageBase : ComponentBase {
		public const string Url = "/user/profile";

		public ProfilePageBase() {
			UserId = "";
			Dispatch = NullDispatch.Instance;
			State = NullAppState.Instance;
			JsRuntime = NullJSRuntime.Instance;
		}

		[Parameter] public string UserId { get; set; }

		[Inject] protected IDispatch Dispatch { get; set; }

		[Inject] protected IAppState State { get; set; }

		[Inject] protected IJSRuntime JsRuntime { get; set; }

		protected ElementReference FileUploadRef { get; set; }

		protected bool ChangingAvatar { get; set; }

		public bool IsTheAuthenticatedUser {
			get {
				if (State.Authentication.User == default) {
					return false;
				}

				var targetId = new Id<ClientUser>( UserId );
				return ( State.Authentication.User.Id.Equals( targetId ) );
			}
		}

		public static string GetUrl( Id<ClientUser> userId ) {
			return $"{Url}/{userId.Value}";
		}
		protected override async Task OnParametersSetAsync() {
			var userId = new Id<ClientUser>( UserId );
			await Dispatch.LoadProfile( userId );
		}

		protected string FormatDate( DateTime? dateTime ) {
			return dateTime
					?.Subtract( TimeSpan.FromHours( 5 ) )
					.ToString( "F", CultureInfo.GetCultureInfo( "en-US" ) )
					?? "None";
		}

		protected void ChangeAvatarClicked() {
			ChangingAvatar = true;
		}

		protected async Task UploadFile() {
			ChangingAvatar = false;

			string data = await JsRuntime.InvokeAsync<string>( "profile.readUploadedFileAsText", FileUploadRef );
			if( !data.StartsWith( "data:" ) ) {
				// No idea what to do here, the browser is behaving strangely...
				return;
			}
			data = data.Substring( "data:".Length );
			string[] parts = data.Split( ',' );
			string[] descriptor = parts[0].Split( ';' );
			string mimeType = descriptor[0];
			string content = parts[1];

			if( mimeType.StartsWith( "image" ) ) {
				await Dispatch.UpdateProfileAvatar( mimeType, content );
			}
		}
	}
}
