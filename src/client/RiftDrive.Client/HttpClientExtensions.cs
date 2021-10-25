using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RiftDrive.Client {
	public static class HttpClientExtensions {
		public static async Task<T> GetJsonAsync<T>( this HttpClient httpClient, Uri requestUri, Func<string, T> fromJson ) where T : class {
			if( httpClient is null ) {
				throw new ArgumentNullException( nameof( httpClient ) );
			}

			if( fromJson is null ) {
				throw new ArgumentNullException( nameof( fromJson ) );
			}

			string responseJson = await httpClient.GetStringAsync( requestUri ).ConfigureAwait( false );
			return fromJson( responseJson );
		}

		public static Task<TResponse> PostJsonAsync<TRequest, TResponse>( this HttpClient httpClient, Uri requestUri, TRequest content, Func<TRequest, string> toJson, Func<string, TResponse> fromJson ) where TRequest : class where TResponse : class
			=> httpClient.SendJsonAsync( HttpMethod.Post, requestUri, content, toJson, fromJson );

		public static Task PostJsonAsync<TRequest>( this HttpClient httpClient, Uri requestUri, TRequest content, Func<TRequest, string> toJson ) where TRequest : class
			=> httpClient.SendJsonAsync<TRequest, IgnoreResponse>( HttpMethod.Post, requestUri, content, toJson, default );

		public static Task<TResponse> PutJsonAsync<TRequest, TResponse>( this HttpClient httpClient, Uri requestUri, TRequest content, Func<TRequest, string> toJson, Func<string, TResponse> fromJson ) where TRequest : class where TResponse : class
			=> httpClient.SendJsonAsync( HttpMethod.Put, requestUri, content, toJson, fromJson );

		public static Task PutJsonAsync<TRequest>( this HttpClient httpClient, Uri requestUri, TRequest content, Func<TRequest, string> toJson ) where TRequest : class
			=> httpClient.SendJsonAsync<TRequest, IgnoreResponse>( HttpMethod.Put, requestUri, content, toJson, default );


		public static async Task<TResponse> SendJsonAsync<TRequest, TResponse>( this HttpClient httpClient, HttpMethod method, Uri requestUri, TRequest content, Func<TRequest, string> toJson, Func<string, TResponse> fromJson ) where TRequest : class where TResponse : class {
			if( toJson is null ) {
				throw new ArgumentNullException( nameof( toJson ) );
			}

			string requestJson = toJson( content );
			using( var requestMessage = new HttpRequestMessage( method, requestUri ) {
				Content = new StringContent( requestJson, Encoding.UTF8, "application/json" )
			} ) {
				HttpResponseMessage response = await httpClient.SendAsync( requestMessage ).ConfigureAwait( false );
				if( ( fromJson is null ) || ( typeof( TResponse ) == typeof( IgnoreResponse ) ) ) {
					return default;
				} else {
					string responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait( false );
					return fromJson( responseJson );
				}
			}
		}

		[SuppressMessage( "Performance", "CA1812", Justification = "Used by method above." )]
		private class IgnoreResponse { }
	}
}
