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
	public sealed class FocusValue: IEquatable<FocusValue> {

		public static FocusValue None = new FocusValue( Focus.None, int.MinValue );

		[JsonConstructor]
		public FocusValue(
			Focus focus,
			int value
		) {
			Focus = focus;
			Value = value;
		}

		public Focus Focus { get; }

		public int Value { get; }

		public bool Equals( FocusValue other ) {
			if( ReferenceEquals( other, this ) ) {
				return true;
			}

			return Focus == other.Focus
				&& Value == other.Value;
		}

		public override bool Equals( object obj ) {
			FocusValue? card = obj as FocusValue;

			if( card == default ) {
				return false;
			}

			return Equals( card );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + Focus.GetHashCode();
				result = ( result * 31 ) + Value.GetHashCode();

				return result;
			}
		}
	}
}
