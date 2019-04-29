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
using Microsoft.AspNetCore.Components;
using RiftDrive.Client.Pages.AuthPages;
using RiftDrive.Client.Pages.UserPages;
using RiftDrive.Client.State;

namespace RiftDrive.Client.Pages.Components {
	public class NavBarComponent : ComponentBase {

		[Inject] protected IConfig Config { get; set; }

		[Inject] protected IAppState AppState { get; set; }

		[Parameter] protected string Username { get; set; }

		[Parameter] protected bool IsAuthenticated { get; set; }

		public string ProfileUrl {
			get {
				return ProfilePageBase.Url;
			}
		}

		public string LogInUrl {
			get {
				return $"{Config.LogInUrl}&redirect_uri={Config.RedirectUrl}";
			}
		}

		public string SignUpUrl {
			get {
				return $"{Config.SignUpUrl}&redirect_uri={Config.RedirectUrl}";
			}
		}

		public string LogOutUrl {
			get {
				return $"{Config.LogOutUrl}&logout_uri={Config.Host}{LogOutPageBase.Url}";
			}
		}
	}
}
