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
using System.Linq;
using System.Threading.Tasks;
using RiftDrive.Client.Service;
using RiftDrive.Client.State;
using RiftDrive.Shared.Model;

namespace RiftDrive.Client.Action {
	internal sealed class Dispatch: IDispatch {

		private readonly IAppState _state;
		private readonly IGameApiService _gameService;

		public Dispatch(
			IAppState state,
			IGameApiService gameService
		) {
			_state = state;
			_gameService = gameService;
		}

		public async Task LoadCurrentGame( Id<Game> gameId ) {
			Game game = await _gameService.GetGame( gameId );
			IEnumerable<Actor> crew = await _gameService.GetCrew( gameId );
			Mothership mothership = await _gameService.GetMothership( gameId );
			IEnumerable<MothershipAttachedModule> modules = await _gameService.GetMothershipModules( gameId, mothership.Id );

			await _state.Update(
				_state.CurrentGame,
				game,
				crew,
				mothership,
				modules );
		}

		public async Task LoadCurrentMission( Id<Game> gameId ) {
			Mission mission = await _gameService.GetMission( gameId );

			await _state.Update(
				_state.CurrentMission,
				mission );
		}

		public async Task TriggerModuleAction( Id<Game> gameId, Id<Mothership> mothershipId, Id<MothershipModule> moduleId, Id<MothershipModuleAction> actionId ) {
			IEnumerable<string> log = await _gameService.TriggerAction( gameId, mothershipId, moduleId, actionId );
			Mothership mothership = await _gameService.GetMothership( gameId );
			IEnumerable<MothershipAttachedModule> modules = await _gameService.GetMothershipModules( gameId, mothershipId );

			await _state.Update( _state.CurrentGame, mothership, modules, log );
		}

		public async Task SelectMissionCrew( Id<Game> gameId, Id<Mission> missionId, IEnumerable<Actor> crew ) {
			Mission mission = await _gameService.SelectMissionCrew( gameId, missionId, crew.Select( c => c.Id ) );
			await _state.Update( _state.CurrentMission, crew );
			await _state.Update( _state.CurrentMission, mission );
		}

		public async Task LogOut() {
			await _state.ClearState();
		}

		public async Task UpdateTokens( string accessToken, string refreshToken, DateTime tokensExpireAt ) {
			await _state.Update( _state.Authentication, accessToken, refreshToken, tokensExpireAt );
		}

	}
}
