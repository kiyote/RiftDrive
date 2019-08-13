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
using System.Linq;
using Microsoft.AspNetCore.Components;
using RiftDrive.Shared.Model;

#nullable enable

namespace RiftDrive.Client.Pages.PlayPages.Components {
	public class RaceEncounterComponent : ComponentBase {

		public RaceEncounterComponent() {
			Crew = new List<Actor>();
			EncounterInteractionId = Id<EncounterInteraction>.Empty;
		}

		[Parameter] public Game? Game { get; set; }

		[Parameter] public Mission? Mission { get; set; }

		[Parameter] public IEnumerable<Actor> Crew { get; set; }

		protected EncounterCard? Card { get; set; }

		protected bool SelectionMade {
			get {
				return ( EncounterInteractionId != Id<EncounterInteraction>.Empty );
			}
		}

		private Id<EncounterInteraction> EncounterInteractionId { get; set; }

		protected override void OnParametersSet() {
			if( Mission != default ) {
				Card = EncounterCard.GetById( Mission.EncounterCardId );
			}
		}

		protected void SelectionChanged( Id<EncounterInteraction> encounterInteractionId ) {
			EncounterInteractionId = encounterInteractionId;
		}

		protected void InteractionSelected( UIMouseEventArgs args ) {
			if( Mission == default || Card == default ) {
				return;
			}

			int magnitude = 0;
			EncounterInteraction interaction = Card.Interactions.First( i => i.Id.Equals( EncounterInteractionId ) );
			if( interaction.Outcomes.Skill != Skill.None ) {
				Role targetRole = interaction.Outcomes.Skill.ToRole();
				Actor crew = Crew.First( c => c.Role == targetRole );
				//TODO: Perform skill check
				magnitude = interaction.Outcomes.Success; // TODO: Use result of skill check
			} else {
				magnitude = interaction.Outcomes.Success;
			}

			EncounterOutcomeCard outcome = EncounterOutcomeCard.GetById( Mission.EncounterOutcomeCardId );
			EncounterOutcome result = outcome.GetResult( magnitude );
			//TODO: Handle the result here
		}
	}
}
