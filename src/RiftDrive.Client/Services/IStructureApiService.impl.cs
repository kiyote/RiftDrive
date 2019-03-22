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
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RiftDrive.Client.Services {
	internal sealed class StructureApiService : IStructureApiService {

		/*
		private const string StructureApiUrl = "/api/structure";
		private readonly HttpClient _http;
		private readonly IAccessTokenProvider _accessTokenProvider;
		private readonly IConfig _config;
		private readonly IJsonConverter _json;

		public StructureApiService(
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

		async Task<IEnumerable<View>> IStructureApiService.GetAllViews() {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetJwtToken() );
			var response = await _http.GetJsonAsync( $@"{_config.Host}{StructureApiUrl}/views",
				( s ) => { return _json.Deserialize<View[]>( s ); } );

			return response;
		}

		async Task<IEnumerable<View>> IStructureApiService.GetUserViews() {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetJwtToken() );
			var response = await _http.GetJsonAsync( $@"{_config.Host}{StructureApiUrl}/view",
				( s ) => { return _json.Deserialize<View[]>( s ); } );

			return response;
		}

		async Task<View> IStructureApiService.CreateView(
			string viewType,
			string viewName
		) {
			if( string.IsNullOrWhiteSpace( viewType ) ) {
				throw new ArgumentException( nameof( viewType ) );
			}

			if( string.IsNullOrWhiteSpace( viewName ) ) {
				throw new ArgumentException( nameof( viewName ) );
			}

			var newView = new View( Id<View>.Empty, viewType, viewName );

			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetJwtToken() );
			var response = await _http.PostJsonAsync( $@"{_config.Host}{StructureApiUrl}/view",
				newView,
				( v ) => { return _json.Serialize( v ); },
				( s ) => { return _json.Deserialize<View>( s ); } );

			return response;
		}

		async Task<Structure> IStructureApiService.CreateStructureInView(
			Id<View> viewId,
			string structureType,
			string name
		) {
			if( viewId == default ) {
				throw new ArgumentException( nameof( viewId ) );
			}

			if( string.IsNullOrWhiteSpace( structureType ) ) {
				throw new ArgumentException( nameof( structureType ) );
			}

			if( string.IsNullOrWhiteSpace( name ) ) {
				throw new ArgumentException( nameof( name ) );
			}

			var newStructure = new Structure( Id<Structure>.Empty, structureType, name );
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetJwtToken() );
			var response = await _http.PostJsonAsync( $@"{_config.Host}{StructureApiUrl}/view/{viewId.Value}",
				newStructure,
				( v ) => { return _json.Serialize( v ); },
				( s ) => { return _json.Deserialize<Structure>( s ); } );

			return response;
		}

		async Task IStructureApiService.AddStructureToView(
			Id<View> viewId,
			Id<Structure> structureId
		) {
			if( viewId == default ) {
				throw new ArgumentException( nameof( viewId ) );
			}

			if( structureId == default ) {
				throw new ArgumentException( nameof( structureId ) );
			}

			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetJwtToken() );
			await _http.PostJsonAsync( $@"{_config.Host}{StructureApiUrl}/{structureId}/view/{viewId}",
				default( Structure ),
				( v ) => { return _json.Serialize( default ); });
		}

		async Task<Structure> IStructureApiService.GetParentStructure(
			Id<View> viewId,
			Id<Structure> structureId
		) {
			if( viewId == default ) {
				throw new ArgumentException( nameof( viewId ) );
			}

			if( structureId == default ) {
				throw new ArgumentException( nameof( structureId ) );
			}

			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetJwtToken() );
			var response = await _http.GetJsonAsync( $@"{_config.Host}{StructureApiUrl}/{structureId}/view/{viewId}",
				( s ) => { return _json.Deserialize<Structure>( s ); } );

			return response;
		}

		async Task<IEnumerable<Structure>> IStructureApiService.GetViewStructures(
			Id<View> viewId
		) {
			if( viewId == default ) {
				throw new ArgumentException( nameof( viewId ) );
			}

			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetJwtToken() );
			var response = await _http.GetJsonAsync( $@"{_config.Host}{StructureApiUrl}/view/{viewId.Value}/structure",
				( s ) => { return _json.Deserialize<Structure[]>( s ); } );

			return response;
		}

		async Task<IEnumerable<Structure>> IStructureApiService.GetChildStructures(
			Id<View> viewId,
			Id<Structure> structureId
		) {
			if( viewId == default ) {
				throw new ArgumentException( nameof( viewId ) );
			}

			if( structureId == default ) {
				throw new ArgumentException( nameof( structureId ) );
			}

			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetJwtToken() );
			var response = await _http.GetJsonAsync( $@"{_config.Host}{StructureApiUrl}/view/{viewId}/structure/{structureId}",
				( s ) => { return _json.Deserialize<Structure[]>( s ); } );

			return response;
		}

		async Task<Structure> IStructureApiService.CreateChildStructure(
			Id<View> viewId,
			Id<Structure> structureId,
			string structureType,
			string name
		) {
			if( structureId == default ) {
				throw new ArgumentException( nameof( structureId ) );
			}

			if( viewId == default ) {
				throw new ArgumentException( nameof( viewId ) );
			}

			if( string.IsNullOrWhiteSpace( structureType ) ) {
				throw new ArgumentException( nameof( structureType ) );
			}

			if( string.IsNullOrWhiteSpace( name ) ) {
				throw new ArgumentException( nameof( name ) );
			}

			var structure = new Structure( Id<Structure>.Empty, structureType, name );
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetJwtToken() );
			var response = await _http.PostJsonAsync( $@"{_config.Host}{StructureApiUrl}/view/{viewId}/structure/{structureId}",
				structure,
				( s ) => { return _json.Serialize( s ); },
				( r ) => { return _json.Deserialize<Structure>( r ); } );

			return response;
		}
		*/
	}
}
