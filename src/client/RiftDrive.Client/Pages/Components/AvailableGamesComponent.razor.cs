using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RiftDrive.Common.Model;

namespace RiftDrive.Client.Pages.Components {
	public class AvailableGamesComponentBase : ComponentBase {
		[Parameter] public User User { get; set; }

		[Parameter] public IEnumerable<Game> Games { get; set; }
	}
}
