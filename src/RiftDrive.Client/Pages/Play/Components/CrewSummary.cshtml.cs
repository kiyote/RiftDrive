using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RiftDrive.Client.Model;
using RiftDrive.Client.Service;
using RiftDrive.Shared;

namespace RiftDrive.Client.Pages.Play.Components
{
    public class CrewSummaryComponent: ComponentBase
    {
		public CrewSummaryComponent() {
			Crew = new List<Actor>();
		}

#nullable disable
		[Inject] protected IGameApiService GameService { get; set; }

		[Parameter] protected Game Game { get; set; }
#nullable enable

		public IEnumerable<Actor> Crew { get; set; }

		protected override async Task OnInitAsync() {
			Crew = await GameService.GetCrew( Game.Id );
		}
	}
}
