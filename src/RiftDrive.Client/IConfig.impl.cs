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
namespace RiftDrive.Client {
	internal sealed class Config : IConfig {
#if DEBUG
		private readonly string _host = "http://localhost:55020";
		private static readonly string _cognitoUrl = "https://riftdrive-development.auth.us-east-1.amazoncognito.com";
#else
		private static readonly string _host = "";
		private static readonly string _cognitoUrl = "https://riftdrive.auth.us-east-1.amazoncognito.com";
#endif


		private static readonly string _tokenUrl = $"{_cognitoUrl}/oauth2/token";

		private static readonly string _cognitoClientId = "4op5jt8qoeav76gins2l9qbfu2";

		private static readonly string _logInUrl = $"{_cognitoUrl}/login?response_type=code&client_id={_cognitoClientId}";

		private static readonly string _signUpUrl = $"{_cognitoUrl}/signup?response_type=code&client_id={_cognitoClientId}";

		string IConfig.Host => _host;

		string IConfig.CongnitoUrl => _cognitoUrl;

		string IConfig.TokenUrl => _tokenUrl;

		string IConfig.LogInUrl => _logInUrl;

		string IConfig.SignUpUrl => _signUpUrl;

		string IConfig.CognitoClientId => _cognitoClientId;
	}
}
