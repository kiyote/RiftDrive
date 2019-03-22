using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RiftDrive.Client.Pages.Auth;

namespace RiftDrive.Client.Pages.Components.NavBar {
	public class NavBarComponent : ComponentBase {

		[Inject] private IConfig _config { get; set; }

		public string LogInUrl {
			get {
				return $"{_config.LogInUrl}&redirect_uri={_config.Host}{ValidateComponent.Url}";
			}
		}

		public string SignUpUrl {
			get {
				return $"{_config.SignUpUrl}&redirect_uri={_config.Host}{ValidateComponent.Url}";
			}
		}
	}
}
