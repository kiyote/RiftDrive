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
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RiftDrive.Client.Model;

namespace RiftDrive.Client.Service {
	internal sealed class TokenService : ITokenService {

		private readonly HttpClient _http;
		private readonly IConfig _config;
		private readonly IJsonConverter _json;

		public TokenService(
			HttpClient httpClient,
			IConfig config,
			IJsonConverter json
		) {
			_http = httpClient;
			_config = config;
			_json = json;
		}

		async Task<AuthorizationToken?> ITokenService.GetToken( string code ) {
			var content = new FormUrlEncodedContent( new List<KeyValuePair<string, string>>() {
				new KeyValuePair<string, string>("grant_type", "authorization_code"),
				new KeyValuePair<string, string>("client_id", _config.CognitoClientId),
				new KeyValuePair<string, string>("code", code),
				new KeyValuePair<string, string>("redirect_uri", _config.RedirectUrl)
			} );
			HttpResponseMessage response = await _http.PostAsync( _config.TokenUrl, content );
			if( response.IsSuccessStatusCode ) {
				string payload = await response.Content.ReadAsStringAsync();
				AuthorizationToken tokens = _json.Deserialize<AuthorizationToken>( payload );

				return tokens;
			}

			return default;
		}

		async Task<AuthorizationToken?> ITokenService.RefreshToken( string refreshToken ) {
			var content = new FormUrlEncodedContent( new List<KeyValuePair<string, string>>() {
				new KeyValuePair<string, string>("grant_type", "refresh_token"),
				new KeyValuePair<string, string>("client_id", _config.CognitoClientId),
				new KeyValuePair<string, string>("refresh_token", refreshToken)
			} );
			HttpResponseMessage response = await _http.PostAsync( _config.TokenUrl, content );
			if( response.IsSuccessStatusCode ) {
				string payload = await response.Content.ReadAsStringAsync();
				AuthorizationToken tokens = _json.Deserialize<AuthorizationToken>( payload );

				return tokens;
			}

			return default;
		}
	}
}
