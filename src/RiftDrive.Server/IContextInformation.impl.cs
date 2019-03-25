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
using Microsoft.AspNetCore.Http;

namespace RiftDrive.Server {
	internal sealed class ContextInformation: IContextInformation {

		private readonly IHttpContextAccessor _httpContextAccessor;

		public ContextInformation( IHttpContextAccessor httpContextAccessor) {
			_httpContextAccessor = httpContextAccessor;
		}

		public string Username {
			get {
				var context = _httpContextAccessor.HttpContext;
				return context.Items[ "Username" ] as string;
			}
		}

		public string Name {
			get {
				var context = _httpContextAccessor.HttpContext;
				return context.Items["Name"] as string;
			}
		}

		public string UserId {
			get {
				var context = _httpContextAccessor.HttpContext;
				return context.Items[ "UserId" ] as string;
			}
		}
	}
}
