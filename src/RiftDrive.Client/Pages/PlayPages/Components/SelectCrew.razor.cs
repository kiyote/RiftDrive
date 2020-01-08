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
	public class SelectCrewComponent : ComponentBase {

		public SelectCrewComponent() {
			SelectedCrew = new List<Id<Actor>>();
			Dispatch = NullDispatch.Instance;
			Crew = new List<Actor>();
		}

		[Parameter] public IEnumerable<Actor> Crew { get; set; }

		[Parameter] public Game? Game { get; set; }

		[Parameter] public Mission? Mission { get; set; }

		[Inject] protected IDispatch Dispatch { get; set; }

		protected List<Id<Actor>> SelectedCrew { get; set; }

		protected async Task SelectClicked( MouseEventArgs args ) {
			if( ( Game == default ) || ( Mission == default ) ) {
				Console.WriteLine( "What?" );
				return;
			}

			if( SelectedCrew.Any() ) {
				IEnumerable<Actor> selectedCrew = Crew.Where( c => SelectedCrew.Contains( c.Id ) );
				await Dispatch.SelectMissionCrew( Game.Id, Mission.Id, selectedCrew );
			}
		}

		protected void SelectionChanged( Id<Actor> crewId ) {
			if( SelectedCrew.Contains( crewId ) ) {
				SelectedCrew.Remove( crewId );
			} else {
				SelectedCrew.Add( crewId );
			}
		}
	}
}
