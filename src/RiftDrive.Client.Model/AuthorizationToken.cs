﻿/*
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
using Newtonsoft.Json;

namespace RiftDrive.Client.Model {
	public class AuthorizationToken {

		[JsonConstructor]
		public AuthorizationToken(
			string id_token,
			string access_token,
			string token_type,
			int expires_in,
			string refresh_token
		) {
			this.id_token = id_token;
			this.access_token = access_token;
			this.token_type = token_type;
			this.expires_in = expires_in;
			this.refresh_token = refresh_token;
		}

		public string id_token { get; }
		public string access_token { get; }
		public string token_type { get; }
		public int expires_in { get; }
		public string refresh_token { get; }
	}
}
