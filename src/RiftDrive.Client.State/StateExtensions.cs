﻿/*
 * Copyright 2018-2020 Todd Lang
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
using Microsoft.Extensions.DependencyInjection;

namespace RiftDrive.Client.State {
	public static class StateExtensions {
		public static IServiceCollection RegisterState<S>( this IServiceCollection services ) where S: class, IStateStorage {
			services.AddSingleton<IStateStorage, S>();
			services.AddSingleton<IAppState, AppState>();

			return services;
		}
	}
}
