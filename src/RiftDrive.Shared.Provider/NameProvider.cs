using System;
using System.Collections.Generic;
using System.Text;

namespace RiftDrive.Shared.Provider {
	internal sealed class NameProvider: INameProvider {

		private readonly IRandomProvider _random;

		public NameProvider(IRandomProvider random) {
			_random = random;
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
				"stopper"
			};

			return suffixes[_random.Next( suffixes.Count )];
		}
	}
}
