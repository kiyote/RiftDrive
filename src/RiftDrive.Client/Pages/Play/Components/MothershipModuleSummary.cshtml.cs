using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RiftDrive.Client.Service;
using RiftDrive.Shared;

namespace RiftDrive.Client.Pages.Play.Components {
	public class MothershipModuleSummaryComponent : ComponentBase {

		public MothershipModuleSummaryComponent() {
			Modules = new List<MothershipAttachedModule>();
		}

#nullable disable
		[Parameter] protected Mothership Mothership { get; set; }

		[Inject] protected IGameApiService GameService { get; set; }
#nullable enable

		protected IEnumerable<MothershipAttachedModule> Modules { get; set; }

		protected override async Task OnInitAsync() {
			Modules = await GameService.GetMothershipModules( Mothership.GameId, Mothership.Id );
		}

		protected async Task ModuleButtonClicked(
			MothershipAttachedModule module,
			MothershipModuleAction action
		) {
			MothershipModule definition = MothershipModule.GetById( module.MothershipModuleId );
			await GameService.TriggerAction( Mothership.GameId, Mothership.Id, module.MothershipModuleId, action.Id );
		}
	}
}
