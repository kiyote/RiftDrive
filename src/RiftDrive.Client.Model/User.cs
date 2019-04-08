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
using RiftDrive.Shared;
using Newtonsoft.Json;

namespace RiftDrive.Client.Model {
	public class User : IEquatable<User> {

		[JsonConstructor]
		public User(
			Id<User> id,
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

		public Id<User> Id { get; }

		public string Username { get; }

		public string? AvatarUrl { get; }

		public DateTime LastLogin { get; }

		public DateTime? PreviousLogin { get; }

		public string Name { get; }

		public bool Equals( User other ) {
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
			if( !( obj is User target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + Id.GetHashCode();
				result = ( result * 31 ) + Username.GetHashCode();
				result = ( result * 31 ) + AvatarUrl?.GetHashCode() ?? 0;
				result = ( result * 31 ) + LastLogin.GetHashCode();
				result = ( result * 31 ) + PreviousLogin?.GetHashCode() ?? 0;
				result = ( result * 13 ) + Name.GetHashCode();

				return result;
			}
		}
	}
}
