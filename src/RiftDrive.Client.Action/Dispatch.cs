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
using System.Text;
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
			await _state.Update( _state.CurrentGame, game );

			IEnumerable<Actor> crew = await _gameService.GetCrew( gameId );
			await _state.Update( _state.CurrentGame, crew );

			Mothership mothership = await _gameService.GetMothership( gameId );
			await _state.Update( _state.CurrentGame, mothership );

			IEnumerable<MothershipAttachedModule> modules = await _gameService.GetMothershipModules( gameId, mothership.Id );
			await _state.Update( _state.CurrentGame, modules );
		}

		public async Task TriggerModuleAction( Id<Game> gameId, Id<Mothership> mothershipId, Id<MothershipModule> moduleId, Id<MothershipModuleAction> actionId ) {
			IEnumerable<string> log = await _gameService.TriggerAction( gameId, mothershipId, moduleId, actionId );
			await _state.Update( _state.CurrentGame, log );

			Mothership mothership = await _gameService.GetMothership( gameId );
			await _state.Update( _state.CurrentGame, mothership );

			IEnumerable<MothershipAttachedModule> modules = await _gameService.GetMothershipModules( gameId, mothershipId );
			await _state.Update( _state.CurrentGame, modules );
		}

	}
}
