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
using Newtonsoft.Json;

namespace RiftDrive.Shared.Model.Client {
	public class ClientUser : IEquatable<ClientUser> {

		[JsonConstructor]
		public ClientUser(
			Id<ClientUser> id,
			string username,
			string? avatarUrl,
			DateTime lastLogin,
			DateTime? previousLogin,
			string name
		) {
			Id = id;
			Username = username;
			AvatarUrl = avatarUrl;
			LastLogin = lastLogin;
			PreviousLogin = previousLogin;
			Name = name;
		}

		public Id<ClientUser> Id { get; }

		public string Username { get; }

		public string? AvatarUrl { get; }

		public DateTime LastLogin { get; }

		public DateTime? PreviousLogin { get; }

		public string Name { get; }

		public bool Equals( ClientUser other ) {
			if (other is null) {
				return false;
			}

			if( ReferenceEquals( other, this ) ) {
				return true;
			}

			return Id.Equals( other.Id )
				&& string.Equals( Username, other.Username, StringComparison.Ordinal )
				&& string.Equals( AvatarUrl, other.AvatarUrl, StringComparison.Ordinal )
				&& DateTime.Equals( LastLogin, other.LastLogin )
				&& Nullable.Equals( PreviousLogin, other.PreviousLogin )
				&& string.Equals( Name, other.Name, StringComparison.Ordinal );
		}

		public override bool Equals( object obj ) {
			if( !( obj is ClientUser target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			return HashCode.Combine( Id, Username, AvatarUrl, LastLogin, PreviousLogin, Name );
		}
	}
}
