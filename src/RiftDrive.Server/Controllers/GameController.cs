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
using GameStartInformation = RiftDrive.Client.Model.GameStartInformation;
using GameCreationInformation = RiftDrive.Client.Model.GameCreationInformation;
using ClientPlayer = RiftDrive.Client.Model.Player;
using ClientMothership = RiftDrive.Client.Model.Mothership;

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
		public async Task<ActionResult<Game>> CreateGame( [FromBody] GameCreationInformation gameInfo ) {
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
		public async Task<ActionResult<Game>> StartGame( string gameId, [FromBody] GameStartInformation gameInfo ) {
			Game game = await _gameManager.StartGame( new Id<Game>( gameId ) );

			return Ok( game );
		}

		[HttpGet( "{gameId}" )]
		public async Task<ActionResult<Game>> GetGame( string gameId ) {
			Game game = await _gameManager.GetGame( new Id<Game>( gameId ) );

			return Ok( game );
		}

		[HttpGet( "{gameId}/player" )]
		public async Task<ActionResult<IEnumerable<ClientPlayer>>> GetPlayers( string gameId ) {
			IEnumerable<ClientPlayer> players = await _gameManager.GetPlayers( new Id<Game>( gameId ) );

			return Ok( players );
		}

		[HttpGet( "{gameId}/mothership" )]
		public async Task<ActionResult<ClientMothership>> GetMothership( string gameId ) {
			ClientMothership mothership = await _gameManager.GetMothership( new Id<Game>( gameId ) );

			return Ok( mothership );
		}

		[HttpGet( "{gameId}/crew" )]
		public async Task<ActionResult<IEnumerable<Actor>>> GetCrew( string gameId ) {
			IEnumerable<Actor> crew = await _gameManager.GetCrew( new Id<Game>( gameId ) );
			return Ok( crew );
		}
	}
}
