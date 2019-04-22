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
using RiftDrive.Server.Model;
using RiftDrive.Server.Service;
using RiftDrive.Shared;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using ClientUser = RiftDrive.Client.Model.User;
using Image = RiftDrive.Server.Model.Image;
using LaborImage = SixLabors.ImageSharp.Image;

namespace RiftDrive.Server.Managers {
	public sealed class UserManager {

		private readonly IIdentificationService _identificationService;
		private readonly IImageService _imageService;

		public UserManager(
			IIdentificationService identificationService,
			IImageService imageService
		) {
			_identificationService = identificationService;
			_imageService = imageService;
		}

		public async Task<ClientUser> RecordLogin( string username ) {
			User user = await _identificationService.RecordLogin( username );

			return ToApiUser( user, await GetAvatarUrl( user ) );
		}

		public async Task<ClientUser> GetUser( string userId ) {
			var id = new Id<User>( userId );
			User user = await _identificationService.GetUser( id );

			return ToApiUser( user, await GetAvatarUrl( user ) );
		}

		public async Task<string> SetAvatar( string userId, string contentType, string content ) {
			using( var image = LaborImage.Load( Convert.FromBase64String( content ) ) ) {
				if( ( image.Width != 256 ) || ( image.Height != 256 ) ) {
					var options = new ResizeOptions() {
						Mode = ResizeMode.Max,
						Size = new Size( 256, 256 )
					};
					content = image.Clone( x => x.Resize( options ) ).ToBase64String( PngFormat.Instance ).Split( ',' )[ 1 ];
					contentType = "image/png";
				}
			}

			// Not a mistake, we're reusing the userId as the imageId for their avatar
			var id = new Id<Image>( userId );
			Image avatar = await _imageService.Update( id, contentType, content );
			if (avatar == default) {
				throw new InvalidOperationException();
			}
			await _identificationService.SetAvatarStatus( new Id<User>( userId ), true );

			return avatar.Url;
		}

		private static ClientUser ToApiUser( User user, string avatarUrl ) {
			return new ClientUser(
				new Id<ClientUser>(user.Id.Value),
				user.Username,
				avatarUrl,
				user.LastLogin,
				user.PreviousLogin,
				user.Name
			);
		}

		private async Task<string> GetAvatarUrl( User user ) {
			if( user.HasAvatar ) {
				// Not a mistake, we're reusing the userId as the imageId for their avatar
				return ( await _imageService.Get( new Id<Image>( user.Id.Value ) ) )?.Url;
			}

			return default;
		}
	}
}
