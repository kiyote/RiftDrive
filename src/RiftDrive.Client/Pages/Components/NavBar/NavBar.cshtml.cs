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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RiftDrive.Client.Pages.Auth;
using RiftDrive.Client.Pages.User;

namespace RiftDrive.Client.Pages.Components.NavBar {
	public class NavBarComponent : ComponentBase {

		[Inject] private IConfig _config { get; set; }

		[Inject] private IAppState _appState { get; set; }

		[Parameter] protected string Name { get; set; }

		[Parameter] protected bool IsAuthenticated { get; set; }

		public string ProfileUrl {
			get {
				return ProfileComponent.Url;
			}
		}

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

		public string LogOutUrl {
			get {
				return $"{_config.LogOutUrl}&logout_uri={_config.Host}{LogOutComponent.Url}";
			}
		}
	}
}
