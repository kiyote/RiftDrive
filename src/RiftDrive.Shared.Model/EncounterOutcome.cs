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

namespace RiftDrive.Shared.Model {
	public sealed class EncounterOutcome: IEquatable<EncounterOutcome> {

		public EncounterOutcome(
			int low,
			int high,
			Id<Ship> shipId,
			EncounterBehaviour behaviour
		) {
			Low = low;
			High = high;
			ShipId = shipId;
			Behaviour = behaviour;
		}

		public int Low { get; }

		public int High { get; }

		public Id<Ship> ShipId { get; }

		public EncounterBehaviour Behaviour { get; }

		public bool Equals( EncounterOutcome other ) {
			if (ReferenceEquals( other, this )) {
				return true;
			}

			return Low == other.Low
				&& High == other.High
				&& ShipId == other.ShipId
				&& Behaviour == other.Behaviour;
		}

		public override bool Equals( object obj ) {
			if( !( obj is EncounterOutcome target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			return HashCode.Combine( Low, High, ShipId, Behaviour );
		}
	}
}
