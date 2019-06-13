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
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace RiftDrive.Client {
	public class NullUriHelper : IUriHelper {

		public static IUriHelper Instance = new NullUriHelper();

		event EventHandler<LocationChangedEventArgs> IUriHelper.OnLocationChanged {
			add {
				throw new NotImplementedException();
			}

			remove {
				throw new NotImplementedException();
			}
		}

		string IUriHelper.GetAbsoluteUri() {
			throw new NotImplementedException();
		}

		string IUriHelper.GetBaseUri() {
			throw new NotImplementedException();
		}

		void IUriHelper.NavigateTo( string uri ) {
			throw new NotImplementedException();
		}

		void IUriHelper.NavigateTo( string uri, bool forceLoad ) {
			throw new NotImplementedException();
		}

		Uri IUriHelper.ToAbsoluteUri( string href ) {
			throw new NotImplementedException();
		}

		string IUriHelper.ToBaseRelativePath( string baseUri, string locationAbsolute ) {
			throw new NotImplementedException();
		}
	}
}
