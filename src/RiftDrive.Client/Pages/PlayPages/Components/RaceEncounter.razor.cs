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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using RiftDrive.Client.Action;
using RiftDrive.Shared.Model;

namespace RiftDrive.Client.Pages.PlayPages.Components {
	public class RaceEncounterComponent : ComponentBase {

		public RaceEncounterComponent() {
			Crew = new List<Actor>();
			EncounterInteractionId = Id<EncounterInteraction>.Empty;
			Dispatch = NullDispatch.Instance;
		}

		[Parameter] public Game? Game { get; set; }

		[Parameter] public Mission? Mission { get; set; }

		[Parameter] public IEnumerable<Actor> Crew { get; set; }

		[Inject] protected IDispatch Dispatch { get; set; }

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

		protected async Task InteractionSelected( MouseEventArgs _ ) {
			if( Game == default || Mission == default || Card == default ) {
				return;
			}

			EncounterInteraction interaction = Card.Interactions.First( i => i.Id.Equals( EncounterInteractionId ) );
			await Dispatch.ResolveEncounterCard( Game.Id, Mission.Id, Card.Id, interaction.Id );
		}
	}
}
