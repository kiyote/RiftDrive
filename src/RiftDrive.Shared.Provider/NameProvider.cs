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
using System.Collections.Generic;

namespace RiftDrive.Shared.Provider {
	internal sealed class NameProvider: INameProvider {

		private readonly IRandomProvider _random;

		public NameProvider(IRandomProvider random) {
			_random = random;
		}

		string INameProvider.CreateActorName() {
			return $"{GetFirstName()} {GetLastName()}";
		}

		private string GetFirstName() {
			var firstNames = new List<string>() {
				"A",
				"B"
			};

			return firstNames[_random.Next( firstNames.Count )];
		}

		private string GetLastName() {
			var lastNames = new List<string>() {
				"C",
				"D"
			};

			return lastNames[_random.Next( lastNames.Count )];
		}

		string INameProvider.CreateMothershipName() {
			return $"{GetPrefix()}{GetSuffix()}";
		}

		private string GetPrefix() {
			var prefixes = new List<string>() {
				"Dawn",
				"Void",
				"Earth",
				"Fire",
				"Wind",
				"Star",
				"Water",
				"Glory",
				"Dusk",
				"Cloud",
				"Nova",
				"Dark",
				"Light",
				"Dust",
				"Hope",
				"Fury",
				"Galaxy",
				"Quantum",
				"Shield"
			};

			return prefixes[_random.Next( prefixes.Count )];
		}

		private string GetSuffix() {
			var suffixes = new List<string>() {
				"strider",
				"striker",
				"walker",
				"shatterer",
				"builder",
				"changer",
				"bringer",
				"taker",
				"eater",
				"stealer",
				"stopper",
				"bearer"
			};

			return suffixes[_random.Next( suffixes.Count )];
		}
	}
}
