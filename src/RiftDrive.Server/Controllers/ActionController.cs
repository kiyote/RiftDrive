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
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RiftDrive.Server.Managers;
using RiftDrive.Server.Model;
using RiftDrive.Shared.Message;
using RiftDrive.Shared.Model;

namespace RiftDrive.Server.Controllers {
	[ResponseCache( Location = ResponseCacheLocation.None, NoStore = true )]
	[Authorize]
	[Route( "api/game/{gameId}/action/" )]
	public class ActionController : Controller {

		private readonly GameManager _gameManager;

		public ActionController(
			GameManager gameManager
		) {
			_gameManager = gameManager;
		}

		[HttpGet( "encounter/{encounterCardId}/interaction/{encounterInteractionId}" )]
		public async Task<ActionResult<EncounterOutcome>> ResolveEncounter(
			string gameId,
			string encounterCardId,
			string encounterInteractionId
		) {
			Mission? mission = await _gameManager.GetMission( new Id<Game>( gameId ) );
			if( mission == default ) {
				return Ok( default );
			}

			EncounterOutcome outcome = await _gameManager.ResolveEncounter(
				new Id<Game>( gameId ),
				mission.Id,
				new Id<EncounterCard>( encounterCardId ),
				new Id<EncounterInteraction>( encounterInteractionId ) );

			return Ok( outcome );
		}

		[HttpGet( "mothership/{mothershipId}/module/{moduleId}/action/{actionId}" )]
		public async Task<ActionResult<IEnumerable<string>>> TriggerModuleAction(
			string gameId,
			string mothershipId,
			string moduleId,
			string actionId
		) {
			IEnumerable<string> log = await _gameManager.TriggerAction(
				new Id<Game>( gameId ),
				new Id<Mothership>( mothershipId ),
				new Id<MothershipModule>( moduleId ),
				new Id<MothershipModuleAction>( actionId ) );

			return Ok( log );
		}
	}
}
