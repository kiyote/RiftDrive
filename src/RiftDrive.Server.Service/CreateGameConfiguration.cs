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
using RiftDrive.Server.Model;
using RiftDrive.Shared.Model;

namespace RiftDrive.Server.Service {
	public sealed class CreateGameConfiguration {

		public CreateGameConfiguration(
			Id<User> createdBy,
			DateTime createdOn,
			string gameName,
			string playerName
		) {
			CreatedBy = createdBy;
			CreatedOn = createdOn.ToUniversalTime();
			GameName = gameName;
			PlayerName = playerName;
		}

		public Id<User> CreatedBy { get; }

		public DateTime CreatedOn { get; }

		public string GameName { get; }

		public string PlayerName { get; }
	}
}
