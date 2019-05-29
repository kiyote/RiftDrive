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
using System.Linq;

namespace RiftDrive.Shared.Model {
	public sealed partial class Race {

		public static Race GetById( Id<Race> raceId ) {
			return All.First( r => r.Id == raceId );
		}

		public static Race Flaxian = new Race( new Id<Race>( "35db9a380e4c40079891fe4a4ff55bda" ), "Flaxian" );

		public static Race Trellak = new Race( new Id<Race>( "adc789765eb34fcb95111ce23cbe0d89" ), "Trellak" );

		public static Race Diylap = new Race( new Id<Race>( "479594f52e7747e0918c36f4b7aa737b" ), "Diylap" );

		public static List<Race> All = new List<Race>() {
			Flaxian,
			Trellak,
			Diylap
		};
	}
}
