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
using RiftDrive.Client.Model;
using RiftDrive.Shared;

namespace RiftDrive.Client.Service {
	internal sealed class GameApiService: IGameApiService {

		private readonly HttpClient _http;
		private readonly IAccessTokenProvider _accessTokenProvider;
		private readonly IConfig _config;
		private readonly IJsonConverter _json;

		public GameApiService(
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

		async Task<Game> IGameApiService.CreateGame( string gameName, string playerName ) {
			var gameInfo = new GameCreationInformation(
				gameName,
				playerName );
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetJwtToken() );
			Game? response = await _http.PostJsonAsync( $@"{_config.Host}/api/game",
				gameInfo,
				( g ) => { return _json.Serialize( g ); },
				( s ) => { return _json.Deserialize<Game>( s ); } );

			if (response == default) {
				throw new InvalidOperationException();
			}

			return response;
		}

		async Task<Game> IGameApiService.StartGame( Id<Game> gameId, string message ) {
			var startInfo = new GameStartInformation(
				message );
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetJwtToken() );
			Game? response = await _http.PostJsonAsync( $@"{_config.Host}/api/game/{gameId.Value}",
				startInfo,
				( g ) => { return _json.Serialize( g ); },
				( s ) => { return _json.Deserialize<Game>( s ); } );

			if (response == default) {
				throw new InvalidOperationException();
			}

			return response;
		}

		async Task<IEnumerable<Game>> IGameApiService.GetGames() {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetJwtToken() );
			Game[] response = await _http.GetJsonAsync( $@"{_config.Host}/api/game",
				( s ) => { return _json.Deserialize<Game[]>( s ); } );

			return response;
		}

		async Task<Game?> IGameApiService.GetGame(Id<Game> gameId) {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetJwtToken() );
			Game? response = await _http.GetJsonAsync( $@"{_config.Host}/api/game/{gameId.Value}",
				( s ) => { return _json.Deserialize<Game>( s ); } );

			return response;
		}

		async Task<IEnumerable<Player>> IGameApiService.GetPlayers(Id<Game> gameId) {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetJwtToken() );
			Player[] response = await _http.GetJsonAsync( $@"{_config.Host}/api/game/{gameId.Value}/player",
				( s ) => { return _json.Deserialize<Player[]>( s ); } );

			return response;
		}

		async Task<Mothership> IGameApiService.GetMothership( Id<Game> gameId ) {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetJwtToken() );
			Mothership response = await _http.GetJsonAsync( $@"{_config.Host}/api/game/{gameId.Value}/mothership",
				( s ) => { return _json.Deserialize<Mothership>( s ); } );

			return response;
		}

		async Task<IEnumerable<Actor>> IGameApiService.GetCrew( Id<Game> gameId ) {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetJwtToken() );
			Actor[] response = await _http.GetJsonAsync( $@"{_config.Host}/api/game/{gameId.Value}/crew",
				( s ) => { return _json.Deserialize<Actor[]>( s ); } );

			return response;
		}
	}
}
