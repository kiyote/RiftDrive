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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RiftDrive.Client.Model;

namespace RiftDrive.Client.Service {
	internal sealed class UserApiService : IUserApiService {
		
		private readonly HttpClient _http;
		private readonly IAccessTokenProvider _accessTokenProvider;
		private readonly IConfig _config;
		private readonly IJsonConverter _json;

		public UserApiService(
			HttpClient http,
			IAccessTokenProvider accessTokenProvider,
			IConfig config,
			IJsonConverter json
		) {
			_http = http;
			_accessTokenProvider = accessTokenProvider;
			_config = config;
			_json = json;
		}

		async Task IUserApiService.RecordLogin() {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetJwtToken() );
			User response = await _http.GetJsonAsync( $@"{_config.Host}/api/user/login",
				( s ) => { return _json.Deserialize<User>( s ); } );
		}

		async Task<User> IUserApiService.GetUserInformation() {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetJwtToken() );
			User response = await _http.GetJsonAsync( $@"{_config.Host}/api/user",
				( s ) => { return _json.Deserialize<User>( s ); } );

			return response;
		}

		async Task<string> IUserApiService.SetAvatar( string contentType, string content ) {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetJwtToken() );
			var request = new AvatarImage(
				contentType,
				content
			);
			AvatarUrl response = await _http.PostJsonAsync( $@"{_config.Host}/api/user/avatar", request,
				( r ) => { return _json.Serialize( r ); },
				( s ) => { return _json.Deserialize<AvatarUrl>( s ); } );

			return response.Url;
		}
		
	}
}
