﻿/*
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
namespace RiftDrive.Server.Model {
	public sealed class AuthenticationUserInformation {

		public AuthenticationUserInformation(
			string authenticationId,
			string username,
			string email,
			string name
		) {
			Username = username;
			Email = email;
			AuthenticationId = authenticationId;
			Name = name;
		}

		public string Username { get; }

		public string Email { get; }

		public string AuthenticationId { get; }

		public string Name { get; }
	}
}
