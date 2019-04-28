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
using Newtonsoft.Json;

namespace RiftDrive.Client.State {
	internal sealed class AuthenticationState : IAuthenticationState {

		public AuthenticationState() {
			Username = "";
			Name = "";
			AccessToken = "";
			RefreshToken = "";
			TokensExpireAt = DateTime.MinValue.ToUniversalTime();
		}

		[JsonConstructor]
        public AuthenticationState(
			string username,
			string name,
            string accessToken,
            string refreshToken,
            DateTime tokensExpireAt
        ) {
            Username = username;
            Name = name;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            TokensExpireAt = tokensExpireAt;
        }

		public string Username { get; }

		public string Name { get; }

		public string AccessToken { get; }

		public string RefreshToken { get; }

		public DateTime TokensExpireAt { get; }

		[JsonIgnore]
		public bool IsAuthenticated {
			get {
				return ( TokensExpireAt >= DateTime.UtcNow );
			}
		}
	}
}
