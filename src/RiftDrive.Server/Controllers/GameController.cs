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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RiftDrive.Server.Managers;
using RiftDrive.Server.Model;
using RiftDrive.Shared;
using ClientGame = RiftDrive.Client.Model.Game;
using ClientPlayer = RiftDrive.Client.Model.Player;

namespace RiftDrive.Server.Controllers {
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
		public async Task<ActionResult<ClientGame>> CreateGame([FromBody] ClientGame clientGame) {
			var userId = new Id<User>( _context.UserId );
			var result = await _gameManager.CreateGame( userId, clientGame.Name, _context.Name );

			return Ok( result );
		}

		[HttpGet()]
		public async Task<ActionResult<IEnumerable<ClientGame>>> GetGames() {
			var userId = new Id<User>( _context.UserId );
			var games = await _gameManager.GetGames( userId );

			return Ok( games );
		}

		[HttpGet("{gameId}/player")]
		public async Task<ActionResult<IEnumerable<ClientPlayer>>> GetPlayers(string gameId) {
			var players = await _gameManager.GetPlayers( new Id<Game>( gameId ) );

			return Ok( players );
		}
	}
}
