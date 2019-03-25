using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RiftDrive.Client.Services;

namespace RiftDrive.Client.Pages.User
{
    public class ProfileComponent : ComponentBase
    {
		public const string Url = "/user/profile";

		[Inject] private IUserApiService _userService { get; set; }

		[Inject] private IJSRuntime _jsRuntime { get; set; }

		protected string AvatarUrl { get; private set; }

		protected string UserId { get; private set; }

		protected string Name { get; private set; }

		protected string LastLogin { get; private set; }

		protected ElementRef FileUploadRef { get; set; }

		protected bool ChangingAvatar { get; set; }

		protected override async Task OnInitAsync() {
			var userInformation = await _userService.GetUserInformation();
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

			var data = await _jsRuntime.InvokeAsync<string>( "profile.readUploadedFileAsText", FileUploadRef );
			if( !data.StartsWith( "data:" ) ) {
				// No idea what to do here, the browser is behaving strangely...
				return;
			}
			data = data.Substring( "data:".Length );
			var parts = data.Split( ',' );
			var descriptor = parts[0].Split( ';' );
			var mimeType = descriptor[0];
			var encoding = descriptor[1];
			var content = parts[1];

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
				AvatarUrl = await _userService.SetAvatar( mimeType, content );
			}
		}
	}
}
