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
using Newtonsoft.Json;

namespace RiftDrive.Shared {
	public sealed class MothershipModuleEffect : IEquatable<MothershipModuleEffect> {

		[JsonConstructor]
		public MothershipModuleEffect(
			ModuleEffect effect,
			int magnitude
		) {
			Effect = effect;
			Magnitude = magnitude;
			RandomMin = int.MinValue;
			RandomMax = int.MinValue;
		}

		public MothershipModuleEffect(
			ModuleEffect effect,
			int randomMin,
			int randomMax
		) {
			Effect = effect;
			Magnitude = int.MinValue;
			RandomMin = randomMin;
			RandomMax = randomMax;
		}

		public ModuleEffect Effect { get; }

		public int Magnitude { get; }

		public int RandomMin { get; }

		public int RandomMax { get; }

		public bool Equals( MothershipModuleEffect other ) {
			if( ReferenceEquals( other, this ) ) {
				return true;
			}

			return Effect == other.Effect
				&& Magnitude == other.Magnitude
				&& RandomMin == other.RandomMin
				&& RandomMax == other.RandomMax;
		}

		public override bool Equals( object obj ) {
			if( !( obj is MothershipModuleEffect target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + Effect.GetHashCode();
				result = ( result * 31 ) + Magnitude.GetHashCode();
				result = ( result * 31 ) + RandomMin.GetHashCode();
				result = ( result * 31 ) + RandomMax.GetHashCode();

				return result;
			}
		}
	}
}