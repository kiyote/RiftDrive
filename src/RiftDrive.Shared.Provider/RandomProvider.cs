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

namespace RiftDrive.Shared.Provider {
	internal sealed class RandomProvider : IRandomProvider {

		private readonly Random _random;

		public RandomProvider() {
			_random = new Random( (int)DateTime.Now.Ticks );
		}

		int IRandomProvider.Next( int max ) {
			return _random.Next( max );
		}

		int IRandomProvider.Next( int min, int max ) {
			int value = _random.Next( max - min + 1 );
			return min + value;
		}

		bool IRandomProvider.Flip() {
			return _random.Next( 2 ) == 1;
		}
	}
}
