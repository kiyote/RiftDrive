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

namespace RiftDrive.Shared.Model {
	public sealed class RoleFocusCheck: IEquatable<RoleFocusCheck> {

		public readonly static RoleFocusCheck None = new RoleFocusCheck( Role.Unknown, FocusCheck.None );

		public RoleFocusCheck(
			Role role,
			FocusCheck focusCheck
		) {
			Role = role;
			FocusCheck = focusCheck;
		}

		public Role Role { get; }

		public FocusCheck FocusCheck { get; }

		public bool Equals( RoleFocusCheck? other ) {
			if (other is null) {
				return false;
			}

			if (ReferenceEquals(other, this)) {
				return true;
			}

			return Role == other.Role
				&& FocusCheck.Equals( other.FocusCheck );
		}

		public override bool Equals( object? obj ) {
			if( !( obj is RoleFocusCheck target ) ) {
				return false;
			}

			return Equals( target as RoleFocusCheck );
		}

		public override int GetHashCode() {
			return HashCode.Combine( Role, FocusCheck );
		}
	}
}
