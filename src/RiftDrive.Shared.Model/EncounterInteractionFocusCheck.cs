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
using Newtonsoft.Json;

namespace RiftDrive.Shared.Model {
	/*
	 * Defines the parameters for a given focus check, indicating the focus
	 * to be tested, the value that is required and the magnitude rewarded
	 * depending on whether the target was met or exceeded (scucess) otherwise
	 * the failure.
	 *
	 * These are used on cards, attached to choices that can be made by the
	 * player.
	 * ie - Build Foo: Engineering(3)
	 * The success and failure values are what will be used to determine
	 * the encounter outcome value.
	 */
	public sealed class EncounterInteractionFocusCheck : IEquatable<EncounterInteractionFocusCheck> {

		public static EncounterInteractionFocusCheck None = new EncounterInteractionFocusCheck( FocusCheck.None, int.MinValue, int.MinValue );

		[JsonConstructor]
		public EncounterInteractionFocusCheck(
			FocusCheck focusCheck,
			int success,
			int failure
		) {
			FocusCheck = focusCheck;
			Success = success;
			Failure = failure;
		}

		public FocusCheck FocusCheck { get; }

		public int Success { get; }

		public int Failure { get; }

		public bool Equals( EncounterInteractionFocusCheck other ) {
			if( ReferenceEquals( other, this ) ) {
				return true;
			}

			return FocusCheck == other.FocusCheck
				&& Success == other.Success
				&& Failure == other.Failure;
		}

		public override bool Equals( object obj ) {
			if( !( obj is EncounterInteractionFocusCheck target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + FocusCheck.GetHashCode();
				result = ( result * 31 ) + Success.GetHashCode();
				result = ( result * 31 ) + Failure.GetHashCode();

				return result;
			}
		}

		public string ToDisplay(bool includeParentheses = true) {
			string result = "-";

			if (this.FocusCheck != FocusCheck.None) {
				result = $"{this.FocusCheck.Focus} {this.FocusCheck.Target}";
			}

			if (includeParentheses) {
				return $"({result})";
			} else {
				return result;
			}
		}
	}
}
