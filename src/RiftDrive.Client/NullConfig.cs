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
using RiftDrive.Client.Service;

namespace RiftDrive.Client {
	public sealed class NullConfig : IConfig {

		public static IConfig Instance = new NullConfig();

		string IConfig.CognitoUrl => throw new NotImplementedException();

		string IConfig.LogInUrl => throw new NotImplementedException();

		string IConfig.SignUpUrl => throw new NotImplementedException();

		string IConfig.LogOutUrl => throw new NotImplementedException();

		string IServiceConfig.Host => throw new NotImplementedException();

		string IServiceConfig.CognitoClientId => throw new NotImplementedException();

		string IServiceConfig.RedirectUrl => throw new NotImplementedException();

		string IServiceConfig.TokenUrl => throw new NotImplementedException();
	}
}
