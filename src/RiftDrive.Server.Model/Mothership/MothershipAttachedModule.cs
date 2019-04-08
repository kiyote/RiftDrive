﻿/*
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
	public sealed class MothershipAttachedModule : IEquatable<MothershipAttachedModule> {

		public MothershipAttachedModule(
			Id<Mothership> mothershipId,
			Id<MothershipModule> mothershipModuleId,
			int remainingPower
		) {
			MothershipId = mothershipId;
			MothershipModuleId = mothershipModuleId;
			RemainingPower = remainingPower;
		}

		public Id<Mothership> MothershipId { get; }

		public Id<MothershipModule> MothershipModuleId { get; }

		public int RemainingPower { get; }

		public bool Equals( MothershipAttachedModule other ) {
			if (ReferenceEquals(other, this)) {
				return true;
			}

			return MothershipId.Equals( other.MothershipId )
				&& MothershipModuleId.Equals( other.MothershipModuleId )
				&& RemainingPower == other.RemainingPower;
		}

		public override bool Equals( object obj ) {
			if( !( obj is MothershipAttachedModule target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + MothershipId.GetHashCode();
				result = ( result * 31 ) + MothershipModuleId.GetHashCode();
				result = ( result * 31 ) + RemainingPower.GetHashCode();

				return result;
			}
		}
	}
}
