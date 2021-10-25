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
using RiftDrive.Shared.Model;

namespace RiftDrive.Server.Model {
	public sealed class Image: IEquatable<Image> {

		public Image(
			Id<Image> imageId,
			string url
		) {
			Id = imageId;
			Url = url;
		}

		public Id<Image> Id { get; }

		public string Url { get; }

		public bool Equals( Image? other ) {
			if( other is null ) {
				return false;
			}

			if( ReferenceEquals(other, this)) {
				return true;
			}

			return Id.Equals( other.Id )
				&& string.Equals( Url, other.Url, StringComparison.OrdinalIgnoreCase );
		}

		public override bool Equals( object? obj ) {
			if( !( obj is Image target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31) + Id.GetHashCode();
				result = ( result * 31 ) + StringComparer.OrdinalIgnoreCase.GetHashCode( Url );

				return result;
			}
		}
	}
}
