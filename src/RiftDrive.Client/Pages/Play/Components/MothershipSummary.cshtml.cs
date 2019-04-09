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
    public class MothershipSummaryComponent : ComponentBase
    {
#nullable disable
        [Inject] protected IGameApiService GameService { get; set; }

		[Parameter] protected string GameId { get; set; }
#nullable enable

		protected Mothership? Mothership { get; set; }

		protected override async Task OnInitAsync() {
			var gameId = new Id<Game>( GameId );
			Mothership = await GameService.GetMothership( gameId );
		}
	}
}
