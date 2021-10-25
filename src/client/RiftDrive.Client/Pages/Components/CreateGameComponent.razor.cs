using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using RiftDrive.Client.Actions;

namespace RiftDrive.Client.Pages.Components {
	public class CreateGameComponentBase: ComponentBase {

		[Inject] public IGameManagementDispatch Dispatch { get; set; }

		protected string GameName { get; set; }

		protected string PlayerName { get; set; }

		protected bool CreatingGame { get; set; }

		public async Task CreateGameClicked( MouseEventArgs _ ) {
			string gameName = GameName;
			string playerName = PlayerName;
			await Dispatch.CreateGameAsync( gameName, playerName );
			GameName = "";
			PlayerName = "";
			CreatingGame = false;
		}

		protected void ShowCreateClicked( MouseEventArgs _ ) {
			CreatingGame = true;
		}
	}
}
