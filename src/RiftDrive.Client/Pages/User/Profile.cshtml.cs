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
using RiftDrive.Client.Service;

namespace RiftDrive.Client.Pages.User {
	public class ProfileComponent : ComponentBase {
		public const string Url = "/user/profile";

		[Inject] protected IUserApiService UserService { get; set; }

		[Inject] protected IJSRuntime JsRuntime { get; set; }

		protected string AvatarUrl { get; private set; }

		protected string UserId { get; private set; }

		protected string Name { get; private set; }

		protected string LastLogin { get; private set; }

		protected ElementRef FileUploadRef { get; set; }

		protected bool ChangingAvatar { get; set; }

		protected override async Task OnInitAsync() {
			Model.User userInformation = await UserService.GetUserInformation();
			Name = userInformation.Name;
			UserId = userInformation.Id.Value;
			AvatarUrl = userInformation.AvatarUrl;
			LastLogin = userInformation.PreviousLogin
					?.Subtract( TimeSpan.FromHours( 5 ) )
					.ToString( "F", CultureInfo.GetCultureInfo( "en-US" ) )
					?? "None";
		}

		protected async Task ChangeAvatarClicked() {
			ChangingAvatar = true;

			await Task.CompletedTask;
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
			//string encoding = descriptor[1];
			string content = parts[1];

			/*
			using( var ms = new System.IO.MemoryStream( 10000 ) ) {
				using( var gzs = new System.IO.Compression.GZipStream( ms, System.IO.Compression.CompressionLevel.Optimal ) ) {
					var bytes = System.Text.Encoding.UTF8.GetBytes( "Content" );
					gzs.Write( bytes, 0, bytes.Length );
				}
			}
			*/

			if( mimeType.StartsWith( "image" ) ) {
				// Cheese it down to 64x64 for now
				//using( var image = Image.Load( Convert.FromBase64String( content ) ) ) {
				//	content = image.Clone( x => x.Resize( 64, 64 ) ).ToBase64String( ImageFormats.Png ).Split( ',' )[ 1 ];
				//	mimeType = "image/png";
				//}
				AvatarUrl = await UserService.SetAvatar( mimeType, content );
			}
		}
	}
}
