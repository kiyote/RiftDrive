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
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RiftDrive.Shared.Model;

namespace RiftDrive.Client.Service {
	internal sealed class ActionApiService : IActionApiService {

		private readonly HttpClient _http;
		private readonly IIdTokenProvider _accessTokenProvider;
		private readonly IServiceConfig _config;
		private readonly IJsonConverter _json;

		public ActionApiService(
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

		async Task<IEnumerable<string>> IActionApiService.TriggerAction(
			Id<Game> gameId,
			Id<Mothership> mothershipId,
			Id<MothershipModule> mothershipModuleId,
			Id<MothershipModuleAction> actionId
		) {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetIdToken() );
			string[]? result = await _http.GetJsonAsync( $@"{_config.Host}/api/game/{gameId.Value}/action/mothership/{mothershipId.Value}/module/{mothershipModuleId.Value}/action/{actionId.Value}",
				( s ) => { return _json.Deserialize<string[]>( s ); } );

			if( result == default ) {
				result = Array.Empty<string>();
			}

			return result;
		}

		async Task<EncounterOutcome> IActionApiService.ResolveEncounterCard(
			Id<Game> gameId,
			Id<Mission> missionId,
			Id<EncounterCard> encounterCardId,
			Id<EncounterInteraction> encounterInteractionId
		) {
			_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", await _accessTokenProvider.GetIdToken() );
			EncounterOutcome? response = await _http.GetJsonAsync( $@"{_config.Host}/api/game/{gameId.Value}/action/encounter/{encounterCardId.Value}/interaction/{encounterInteractionId.Value}",
				( s ) => { return _json.Deserialize<EncounterOutcome>( s ); } );

			if( response == default ) {
				throw new InvalidOperationException();
			}

			return response;
		}
	}
}