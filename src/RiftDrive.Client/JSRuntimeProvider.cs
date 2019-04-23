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
using Microsoft.JSInterop;

namespace RiftDrive.Client {
	internal sealed class JSRuntimeProvider: IJSRuntimeProvider {

		private readonly IServiceProvider _serviceProvider;

		public JSRuntimeProvider(
			IServiceProvider serviceProvider
		) {
			_serviceProvider = serviceProvider;
		}

		public bool TryGet( out IJSRuntime jsRuntime ) {
			jsRuntime = (IJSRuntime)_serviceProvider.GetService( typeof( IJSRuntime ) );
			return ( jsRuntime != default );
		}

		public IJSRuntime Get() {
			IJSRuntime jsRuntime;
			if (!TryGet(out jsRuntime)) {
				throw new InvalidOperationException();
			}

			return jsRuntime;
		}
	}
}
