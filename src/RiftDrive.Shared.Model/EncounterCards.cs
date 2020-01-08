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
using System.Collections.Generic;
using System.Linq;

namespace RiftDrive.Shared.Model {
	public sealed partial class EncounterCard {

		public static EncounterCard GetById( Id<EncounterCard> encounterCardId ) {
			return All.First( c => c.Id == encounterCardId );
		}

		public static EncounterCard Card1 = new EncounterCard(
			new Id<EncounterCard>( "5f077957a5ad4d7b98c33c005e8a50fc" ),
			"As the discharge from the Rift fades from your display it becomes clear that you are not alone.  Nearby, a vessel cruises towards you, clearly attracted by the disturbance your arrival has made.",
			new RoleFocusCheck( Role.Science, new FocusCheck( Focus.Science, 5 )),
			new List<EncounterInteraction>() {
				new EncounterInteraction(
					new Id<EncounterInteraction>("d8926ed3ef89471cb2eae906504a58b1"),
					"Attempt to disguise your vessel by altering its engine signature.",
					new EncounterInteractionCheck( new RoleFocusCheck( Role.Engineer, new FocusCheck( Focus.Engineering, 3 )), 2, 7 )
				),
				new EncounterInteraction(
					new Id<EncounterInteraction>("a79d90a88a7e47baa37aa8996a027fdc"),
					"Broadcast an aggressive warning that approaching vessels will be considered hostile.",
					new EncounterInteractionCheck( new RoleFocusCheck( Role.Command, new FocusCheck( Focus.Command, 5 )), 10, 4 )
				),
				new EncounterInteraction(
					new Id<EncounterInteraction>("a12d1630f8cf43bebedac85df3ca7a74"),
					"Hail the vessel and attempt to communicate.",
					new EncounterInteractionCheck( RoleFocusCheck.None, 1, 1 )
				),
				new EncounterInteraction(
					new Id<EncounterInteraction>("982cc8cda6624c349851a86205e278ca"),
					"Engage engines and flee the incoming vessel.",
					new EncounterInteractionCheck( RoleFocusCheck.None, 3, 3 )
				)
			} );

		public static List<EncounterCard> All = new List<EncounterCard>() {
			Card1
		};
	}
}
