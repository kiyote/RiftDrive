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
using System.Collections.Generic;
using System.Globalization;

namespace RiftDrive.Common.Model {

	public sealed class Id<T> : IEquatable<Id<T>> {

		public static readonly Id<T> Empty = new Id<T>( Guid.Empty.ToString( "N", CultureInfo.InvariantCulture ), false );

		public static readonly IEnumerable<Id<T>> EmptyList = new List<Id<T>>();

		public Id() {
			Value = Guid.NewGuid().ToString( "N", CultureInfo.InvariantCulture );
		}

		public Id( Guid id ) {
			if( id == Guid.Empty ) {
				throw new ArgumentException( "Cannot construct an Id<T> with Guid.Empty.", nameof( id ) );
			}

			Value = id.ToString( "N", CultureInfo.InvariantCulture );
		}

		public Id( string value ) :
			this( value, false ) {
		}

		private Id( string value, bool validate ) {
			if( !validate ) {
				Value = value;
			} else {
				if( string.IsNullOrWhiteSpace( value ) ) {
					throw new ArgumentException( value );
				}

				if( !Guid.TryParse( value, out Guid id ) ) {
					throw new ArgumentException( "Value supplied is not a valid Guid.", nameof( value ) );
				}

				if( id == Guid.Empty ) {
					throw new ArgumentException( "Cannot construct an Id<T> with Guid.Empty.", nameof( value ) );
				}

				Value = id.ToString( "N", CultureInfo.InvariantCulture );
			}

		}

		public string Value { get; set; }

		public override string ToString() {
			return Value;
		}

		public override int GetHashCode() {
			return HashCode.Combine( Value );
		}

		public override bool Equals( object obj ) {
			if( !( obj is Id<T> target ) ) {
				return false;
			}

			return Equals( target );
		}

		public bool Equals( Id<T> other ) {
			if (other is null) {
				return false;
			}

			if( ReferenceEquals( other, this ) ) {
				return true;
			}

			return
				string.Equals( Value, other.Value, StringComparison.Ordinal );
		}

		public static bool operator ==( Id<T> left, Id<T> right ) {
			if( ReferenceEquals( left, right ) ) {
				return true;
			}

			// If one is null, but not both, return false.
			if( ReferenceEquals( left, default( Id<T> ) ) || ReferenceEquals( right, default( Id<T> ) ) ) {
				return false;
			}

			return
				string.Equals( left.Value, right.Value, StringComparison.Ordinal );
		}

		public static bool operator !=( Id<T> left, Id<T> right ) {
			return !( left == right );
		}
	}
}
