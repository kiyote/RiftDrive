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
using RiftDrive.Shared.Model.Client;

namespace RiftDrive.Server.Controllers {
	[ResponseCache( Location = ResponseCacheLocation.None, NoStore = true )]
	[Authorize]
	[Route( "api/game" )]
	public class GameController : Controller {

		private readonly GameManager _gameManager;
		private readonly IContextInformation _context;

		public GameController(
			GameManager gameManager,
			IContextInformation context
		) {
			_gameManager = gameManager;
			_context = context;
		}

		[HttpPost()]
		public async Task<ActionResult<Game>> CreateGame(
			[FromBody] CreateGameRequest gameInfo
		) {
			var userId = new Id<User>( _context.UserId );
			Game result = await _gameManager.CreateGame( userId, gameInfo.GameName, gameInfo.PlayerName );

			return Ok( result );
		}

		[HttpGet()]
		public async Task<ActionResult<IEnumerable<Game>>> GetGames() {
			var userId = new Id<User>( _context.UserId );
			IEnumerable<Game> games = await _gameManager.GetGames( userId );

			return Ok( games );
		}


		[HttpPost( "{gameId}" )]
		public async Task<ActionResult<Game>> StartGame(
			string gameId,
			[FromBody] StartGameRequest gameInfo
		) {
			Game game = await _gameManager.StartGame( new Id<Game>( gameId ) );

			return Ok( game );
		}

		[HttpGet( "{gameId}" )]
		public async Task<ActionResult<Game?>> GetGame(
			string gameId
		) {
			Game? game = await _gameManager.GetGame( new Id<Game>( gameId ) );

			return Ok( game );
		}

		[HttpDelete( "{gameId}" )]
		public async Task<ActionResult> DeleteGame(
			string gameId
		) {
			await _gameManager.DeleteGame( new Id<Game>( gameId ) );
			return Ok();
		}

		[HttpGet( "{gameId}/player" )]
		public async Task<ActionResult<IEnumerable<ClientPlayer>>> GetPlayers(
			string gameId
		) {
			IEnumerable<ClientPlayer> players = await _gameManager.GetPlayers( new Id<Game>( gameId ) );

			return Ok( players );
		}

		[HttpGet( "{gameId}/crew" )]
		public async Task<ActionResult<IEnumerable<Actor>>> GetCrew(
			string gameId
		) {
			IEnumerable<Actor> crew = await _gameManager.GetCrew( new Id<Game>( gameId ) );
			return Ok( crew );
		}

		[HttpGet( "{gameId}/mission" )]
		public async Task<ActionResult<Mission?>> GetMission(
			string gameId
		) {
			Mission? mission = await _gameManager.GetMission( new Id<Game>( gameId ) );
			return Ok( mission );
		}

		[HttpGet( "{gamesId}/mission/encounter" )]
		public async Task<ActionResult<EncounterCard>> GetEncounterCard(
			string gameId
		) {
			Mission? mission = await _gameManager.GetMission( new Id<Game>( gameId ) );
			if (mission == default) {
				return Ok( default );
			}

			return Ok( await _gameManager.GetEncounterCard( new Id<Game>( gameId ), mission.Id ) );
		}

		[HttpPost( "{gameId}/mission/crew" )]
		public async Task<ActionResult<Mission>> SelectMissionCrew(
			string gameId,
			[FromBody] SelectMissionCrewRequest request
		) {
			Mission mission = await _gameManager.AddCrewToMission( request.MissionId, request.Crew );
			return Ok( mission );
		}

		/*
		[HttpGet( "{gameId}/mission/crew/{crewId}/skill/{skillId}" )]
		public async Task<ActionResult> PerformSkillCheck(
			Id<Actor> crewId,
			Skill skill
		) {

		}
		*/


		[HttpGet( "{gameId}/mothership" )]
		public async Task<ActionResult<Mothership?>> GetMothership(
			string gameId
		) {
			Mothership? mothership = await _gameManager.GetMothership( new Id<Game>( gameId ) );

			return Ok( mothership );
		}

		[HttpGet( "{gameId}/mothership/{mothershipId}/module" )]
		public async Task<ActionResult<MothershipAttachedModule>> GetMothershipModules(
			string gameId,
			string mothershipId
		) {
			IEnumerable<MothershipAttachedModule> modules = await _gameManager.GetMothershipModules(
				new Id<Game>( gameId ),
				new Id<Mothership>( mothershipId ) );

			return Ok( modules );
		}

		[HttpGet( "{gameId}/mothership/{mothershipId}/module/{moduleId}/action/{actionId}" )]
		public async Task<ActionResult> TriggerModuleAction(
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
