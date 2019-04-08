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
using System.Collections.Generic;
using System.Text;
using RiftDrive.Shared;

namespace RiftDrive.Server.Model {
	public sealed class MothershipModuleAction : IEquatable<MothershipModuleAction> {

		public MothershipModuleAction(
			Id<MothershipModule> mothershipModuleId,
			ModuleEffect effect,
			int magnitude
		) {
			MothershipModuleId = mothershipModuleId;
			Effect = effect;
			Magnitude = magnitude;
		}

		public Id<MothershipModule> MothershipModuleId { get; }

		public ModuleEffect Effect { get; }

		public int Magnitude { get; }

		public bool Equals( MothershipModuleAction other ) {
			if (ReferenceEquals(other, this)) {
				return true;
			}

			return MothershipModuleId.Equals( other.MothershipModuleId )
				&& Effect == other.Effect
				&& Magnitude == other.Magnitude;
		}

		public override bool Equals( object obj ) {
			if( !( obj is MothershipModuleAction target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + MothershipModuleId.GetHashCode();
				result = ( result * 31 ) + Effect.GetHashCode();
				result = ( result * 31 ) + Magnitude.GetHashCode();

				return result;
			}
		}
	}
}
