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

namespace RiftDrive.Server.Model {
	public sealed class Race: IEquatable<Race> {

		public Race(
			Id<Race> id,
			string name
		) {
			Id = id;
			Name = name;
		}

		public Id<Race> Id { get; }

		public string Name { get; }

		public bool Equals( Race other ) {
			if (other is null) {
				return false;
			}

			if (ReferenceEquals(other, this)) {
				return true;
			}

			return Id.Equals( other.Id )
				&& string.Equals( Name, other.Name, StringComparison.Ordinal );
		}

		public override bool Equals( object obj ) {
			return Equals( obj as Race );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + Id.GetHashCode();
				result = ( result * 31 ) + Name.GetHashCode();

				return result;
			}
		}
	}
}
