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

namespace RiftDrive.Shared.Model {
	/*
	 * Defines a check to be made against a particular focus.  It defines
	 * the focus value to be tested and the magnitude required for success.
	 */
	public sealed class FocusCheck : IEquatable<FocusCheck> {

		public static FocusCheck None = new FocusCheck( Focus.Unknown, int.MinValue );

		[JsonConstructor]
		public FocusCheck(
			Focus focus,
			int target
		) {
			Focus = focus;
			Target = target;
		}

		public Focus Focus { get; }

		public int Target { get; }

		public bool Equals( FocusCheck other ) {
			if( ReferenceEquals( other, this ) ) {
				return true;
			}

			return Focus == other.Focus
				&& Target == other.Target;
		}

		public override bool Equals( object obj ) {
			if( !( obj is FocusCheck target ) ) {
				return false;
			}

			return Equals( target as FocusCheck );
		}

		public override int GetHashCode() {
			return HashCode.Combine( Focus, Target );
		}
	}
}
