/*
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
using System;
using RiftDrive.Client.Pages.Auth;

namespace RiftDrive.Client {
	public sealed class Config {
		public const string ApiHost = "https://localhost:5001";
		public static readonly string CognitoClientId = "4op5jt8qoeav76gins2l9qbfu2";
		private static readonly string _cognitoUrl = "https://riftdrive-development.auth.us-east-1.amazoncognito.com";
		public static readonly string TokenUrl = $"{_cognitoUrl}/oauth2/token";
		public static readonly string RedirectUrl = $"{ApiHost}{ValidatePageBase.Url}";
		public static readonly string LogInUrl = $"{_cognitoUrl}/login?response_type=code&client_id={CognitoClientId}";
		public static readonly string SignUpUrl = $"{_cognitoUrl}/signup?response_type=code&client_id={CognitoClientId}";
		public static readonly string LogOutUrl = $"{_cognitoUrl}/logout?client_id={CognitoClientId}";
	}
}
