using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RiftDrive.Common.Model;
using RiftDrive.Common.Serialization;

namespace RiftDrive.Client.Services.Identification {
	internal sealed class IdentificationService : IIdentificationService {

		private readonly HttpClient _http;
		private readonly IJsonSerializer _json;
		private readonly IdentificationOptions _options;

		public IdentificationService(
			HttpClient httpClient,
			IJsonSerializer jsonSerializer,
			IOptions<IdentificationOptions> options
		) {
			_http = httpClient;
			_json = jsonSerializer;
			_options = options.Value;
		}

		async Task<Tokens> IIdentificationService.GetTokensAsync( string code ) {
			using( var content = new FormUrlEncodedContent( new List<KeyValuePair<string, string>>() {
				new KeyValuePair<string, string>("grant_type", "authorization_code"),
				new KeyValuePair<string, string>("client_id", _options.CognitoClientId),
				new KeyValuePair<string, string>("code", code),
				new KeyValuePair<string, string>("redirect_uri", _options.RedirectUrl)
			} ) ) {
				HttpResponseMessage response = await _http.PostAsync( _options.TokenUrl, content ).ConfigureAwait( false );
				if( response.IsSuccessStatusCode ) {
					string payload = await response.Content.ReadAsStringAsync().ConfigureAwait( false );
					Tokens tokens = _json.Deserialize<Tokens>( payload );

					return tokens;
				}

				throw new InvalidOperationException();
			}
		}

		async Task<Tokens> IIdentificationService.RefreshTokensAsync( string refreshToken ) {
			using( var content = new FormUrlEncodedContent( new List<KeyValuePair<string, string>>() {
				new KeyValuePair<string, string>("grant_type", "refresh_token"),
				new KeyValuePair<string, string>("client_id", _options.CognitoClientId),
				new KeyValuePair<string, string>("refresh_token", refreshToken)
			} ) ) {
				HttpResponseMessage response = await _http.PostAsync( _options.TokenUrl, content ).ConfigureAwait( false );
				if( response.IsSuccessStatusCode ) {
					string payload = await response.Content.ReadAsStringAsync().ConfigureAwait( false );
					Tokens tokens = _json.Deserialize<Tokens>( payload );

					return tokens;
				}

				throw new InvalidOperationException();
			}
		}

		async Task<User> IIdentificationService.RecordLogin( string idToken ) {
			var content = new StringContent( string.Empty );
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", idToken );
			HttpResponseMessage response = await _http.PostAsync( $@"{_options.ApiHost}/api/user/login", content );
			if (response.IsSuccessStatusCode) {
				string payload = await response.Content.ReadAsStringAsync();
				User user = _json.Deserialize<User>( payload );

				return user;
			}

			throw new InvalidOperationException();
		}
	}
}
