/*
 * Copyright 2018-2020 Todd Lang
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
using RiftDrive.Server.Repository;
using RiftDrive.Server.Model;
using RiftDrive.Shared.Model;

namespace RiftDrive.Server.Service {
	internal sealed class IdentificationService : IIdentificationService {

		private readonly IAuthenticationRepository _authenticationRepository;
		private readonly IUserRepository _userRepository;

		public IdentificationService(
			IAuthenticationRepository authenticationRepository,
			IUserRepository userRepository
		) {
			_authenticationRepository = authenticationRepository;
			_userRepository = userRepository;
		}

		async Task<User> IIdentificationService.RecordLogin( string username ) {
			var authenticationInformation = await _authenticationRepository.GetUserInformation( username );

			if (authenticationInformation == default) {
				throw new ArgumentException();
			}

			var user = await _userRepository.GetByUsername( username );
			var lastLogin = DateTime.UtcNow;

			// If they don't have a local record, create one
			if( user == default ) {
				user = await _userRepository.AddUser(
					new Id<User>(),
					authenticationInformation.Username,
					DateTime.UtcNow,
					lastLogin,
					authenticationInformation.Name
				);
			} else {
				user = await _userRepository.SetLastLogin( user.Id, lastLogin );
			}

			return user;
		}

		async Task<User?> IIdentificationService.GetUser( Id<User> userId ) {
			return await _userRepository.GetUser( userId );
		}

		async Task<User> IIdentificationService.SetAvatarStatus( Id<User> userId, bool hasAvatar ) {
			return await _userRepository.UpdateAvatarStatus( userId, hasAvatar );
		}
	}
}
