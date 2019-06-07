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
	public sealed partial class EncounterOutcomeCard {

		public static EncounterOutcomeCard GetById( Id<EncounterOutcomeCard> cardId ) {
			return All.First( c => c.Id.Equals( cardId ) );
		}

		public static EncounterOutcomeCard Flaxian1 = new EncounterOutcomeCard(
			new Id<EncounterOutcomeCard>( "4badd6372dc246cf9a8d242b591d8240" ),
			new List<EncounterOutcome>() {
				new EncounterOutcome( int.MinValue, 2, Ship.FlaxianScout.Id )
			} );

		public static List<EncounterOutcomeCard> All = new List<EncounterOutcomeCard>() {
			Flaxian1
		};
	}
}
