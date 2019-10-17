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
using RiftDrive.Shared.Message;
using RiftDrive.Shared.Model;

namespace RiftDrive.Client.Service {
	internal sealed class GameApiService : IGameApiService {

		private readonly HttpClient _http;
		private readonly IIdTokenProvider _accessTokenProvider;
		private readonly IServiceConfig _config;
		private readonly IJsonConverter _json;

		public GameApiService(
			HttpClient http,
			IIdTokenProvider accessTokenProvider,
			IServiceConfig config,
			IJsonConverter json
		) {
			_http = http;
			_accessTokenProvider = accessTokenProvider;
			_config = config;
			_json = json;
		}

		async Task<Game> IGameApiService.CreateGame( string gameName, string playerName ) {
			var gameInfo = new CreateGameRequest(
				gameName,
				playerName );
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetIdToken() );
			Game? response = await _http.PostJsonAsync( $@"{_config.Host}/api/game",
				gameInfo,
				( g ) => { return _json.Serialize( g ); },
				( s ) => { return _json.Deserialize<Game>( s ); } );

			if( response == default ) {
				throw new InvalidOperationException();
			}

			return response;
		}

		async Task IGameApiService.DeleteGame( Id<Game> gameId ) {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetIdToken() );
			await _http.DeleteAsync( $@"{_config.Host}/api/game/{gameId.Value}" );
		}

		async Task<Game> IGameApiService.StartGame( Id<Game> gameId, string message ) {
			var startInfo = new StartGameRequest(
				message );
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetIdToken() );
			Game? response = await _http.PostJsonAsync( $@"{_config.Host}/api/game/{gameId.Value}",
				startInfo,
				( g ) => { return _json.Serialize( g ); },
				( s ) => { return _json.Deserialize<Game>( s ); } );

			if( response == default ) {
				throw new InvalidOperationException();
			}

			return response;
		}

		async Task<IEnumerable<Game>> IGameApiService.GetGames() {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetIdToken() );
			Game[]? response = await _http.GetJsonAsync( $@"{_config.Host}/api/game",
				( s ) => { return _json.Deserialize<Game[]>( s ); } );

			if( response == default ) {
				response = Array.Empty<Game>();
			}

			return response;
		}

		async Task<Game?> IGameApiService.GetGame( Id<Game> gameId ) {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetIdToken() );
			Game? response = await _http.GetJsonAsync( $@"{_config.Host}/api/game/{gameId.Value}",
				( s ) => { return _json.Deserialize<Game>( s ); } );

			return response;
		}

		async Task<IEnumerable<ClientPlayer>> IGameApiService.GetPlayers( Id<Game> gameId ) {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetIdToken() );
			ClientPlayer[]? response = await _http.GetJsonAsync( $@"{_config.Host}/api/game/{gameId.Value}/player",
				( s ) => { return _json.Deserialize<ClientPlayer[]>( s ); } );

			if (response == default) {
				response = Array.Empty<ClientPlayer>();
			}

			return response;
		}

		async Task<Mothership?> IGameApiService.GetMothership( Id<Game> gameId ) {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetIdToken() );
			Mothership? response = await _http.GetJsonAsync( $@"{_config.Host}/api/game/{gameId.Value}/mothership",
				( s ) => { return _json.Deserialize<Mothership>( s ); } );

			return response;
		}

		async Task<IEnumerable<Actor>> IGameApiService.GetCrew( Id<Game> gameId ) {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetIdToken() );
			Actor[]? response = await _http.GetJsonAsync( $@"{_config.Host}/api/game/{gameId.Value}/crew",
				( s ) => { return _json.Deserialize<Actor[]>( s ); } );

			if (response == default) {
				response = Array.Empty<Actor>();
			}

			return response;
		}

		async Task<IEnumerable<MothershipAttachedModule>> IGameApiService.GetMothershipModules(
			Id<Game> gameId,
			Id<Mothership> mothershipId
		) {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetIdToken() );
			MothershipAttachedModule[]? response = await _http.GetJsonAsync( $@"{_config.Host}/api/game/{gameId.Value}/mothership/{mothershipId.Value}/module",
				( s ) => { return _json.Deserialize<MothershipAttachedModule[]>( s ); } );

			if (response == default) {
				response = Array.Empty<MothershipAttachedModule>();
			}

			return response;
		}

		async Task<IEnumerable<string>> IGameApiService.TriggerAction(
			Id<Game> gameId,
			Id<Mothership> mothershipId,
			Id<MothershipModule> mothershipModuleId,
			Id<MothershipModuleAction> actionId
		) {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetIdToken() );
			string[]? result = await _http.GetJsonAsync( $@"{_config.Host}/api/game/{gameId.Value}/mothership/{mothershipId.Value}/module/{mothershipModuleId.Value}/action/{actionId.Value}",
				( s ) => { return _json.Deserialize<string[]>( s ); } );

			if (result == default) {
				result = Array.Empty<string>();
			}

			return result;
		}

		async Task<Mission?> IGameApiService.GetMission( Id<Game> gameId ) {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetIdToken() );
			Mission? response = await _http.GetJsonAsync( $@"{_config.Host}/api/game/{gameId.Value}/mission",
				( s ) => { return _json.Deserialize<Mission>( s ); } );

			return response;
		}

		async Task<Mission> IGameApiService.SelectMissionCrew( Id<Game> gameId, Id<Mission> missionId, IEnumerable<Id<Actor>> crew ) {
			SelectMissionCrewRequest request = new SelectMissionCrewRequest( missionId, crew );
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetIdToken() );
			Mission? response = await _http.PostJsonAsync( $@"{_config.Host}/api/game/{gameId.Value}/mission/crew",
				request,
				( r ) => { return _json.Serialize( r ); },
				( s ) => { return _json.Deserialize<Mission>( s ); } );

			if (response == default) {
				throw new ArgumentException();
			}

			return response;
		}

		async Task<EncounterCard> IGameApiService.DrawEncounterCard( Id<Game> gameId, Id<Mission> missionId ) {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetIdToken() );
			EncounterCard? response = await _http.GetJsonAsync( $@"{_config.Host}/api/game/{gameId.Value}/mission/encounter",
				( s ) => { return _json.Deserialize<EncounterCard>( s ); } );

			if (response == default) {
				throw new ArgumentException();
			}

			return response;
		}
	}
}
