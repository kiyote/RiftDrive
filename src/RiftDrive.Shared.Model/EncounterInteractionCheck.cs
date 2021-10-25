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
	public sealed class EncounterInteractionCheck : IEquatable<EncounterInteractionCheck> {

		public readonly static EncounterInteractionCheck None = new EncounterInteractionCheck( RoleFocusCheck.None, int.MinValue, int.MinValue );

		[JsonConstructor]
		public EncounterInteractionCheck(
			RoleFocusCheck roleFocusCheck,
			int success,
			int failure
		) {
			RoleFocusCheck = roleFocusCheck;
			Success = success;
			Failure = failure;
		}

		public RoleFocusCheck RoleFocusCheck { get; }

		public int Success { get; }

		public int Failure { get; }

		public bool Equals( EncounterInteractionCheck? other ) {
			if (other is null) {
				return false;
			}

			if( ReferenceEquals( other, this ) ) {
				return true;
			}

			return RoleFocusCheck.Equals( other.RoleFocusCheck )
				&& Success == other.Success
				&& Failure == other.Failure;
		}

		public override bool Equals( object? obj ) {
			if( !( obj is EncounterInteractionCheck target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			return HashCode.Combine( RoleFocusCheck, Success, Failure );
		}

		public string ToDisplay(bool includeParentheses = true) {
			string result = "";

			if (this.RoleFocusCheck != RoleFocusCheck.None) {
				result = $"{this.RoleFocusCheck.Role}: {this.RoleFocusCheck.FocusCheck.Focus} {this.RoleFocusCheck.FocusCheck.Target}";
			}

			if (includeParentheses && !string.IsNullOrWhiteSpace(result)) {
				return $"({result})";
			} else {
				return result;
			}
		}
	}
}
