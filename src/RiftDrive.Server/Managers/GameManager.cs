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
using System.Linq;
using System.Threading.Tasks;
using RiftDrive.Server.Model;
using RiftDrive.Server.Service;
using RiftDrive.Shared.Model;
using RiftDrive.Shared.Model.Client;

namespace RiftDrive.Server.Managers {
	public class GameManager {

		private readonly IGameService _gameService;

		public GameManager(
			IGameService gameService
		) {
			_gameService = gameService;
		}

		public async Task<Game> CreateGame(
			Id<User> userId,
			string gameName,
			string playerName
		) {
			var config = new CreateGameConfiguration(
				userId,
				DateTime.UtcNow,
				gameName,
				playerName );
			Game game = await _gameService.CreateGame( config );

			return game;
		}

		public async Task DeleteGame( Id<Game> gameId ) {
			await _gameService.DeleteGame( gameId );
		}

		public async Task<IEnumerable<Game>> GetGames( Id<User> userId ) {
			IEnumerable<Game> games = await _gameService.GetGames( userId );

			return games;
		}

		public async Task<Game?> GetGame( Id<Game> gameId ) {
			Game? game = await _gameService.GetGame( gameId );

			return game;
		}

		public async Task<Game> StartGame( Id<Game> gameId ) {
			Game game = await _gameService.StartGame( gameId );

			return game;
		}

		public async Task<IEnumerable<ClientPlayer>> GetPlayers( Id<Game> gameId ) {
			IEnumerable<Player> players = await _gameService.GetPlayers( gameId );

			return players.Select( p => ToClientPlayer( p ) );
		}

		public async Task<Mothership?> GetMothership( Id<Game> gameId ) {
			return await _gameService.GetMothership( gameId );
		}

		public async Task<IEnumerable<MothershipAttachedModule>> GetMothershipModules( Id<Game> gameId, Id<Mothership> mothershipId ) {
			return await _gameService.GetMothershipModules( gameId, mothershipId );
		}

		public async Task<IEnumerable<Actor>> GetCrew( Id<Game> gameId ) {
			return await _gameService.GetCrew( gameId );
		}

		public async Task<IEnumerable<string>> TriggerAction(
			Id<Game> gameId,
			Id<Mothership> mothershipId,
			Id<MothershipModule> moduleId,
			Id<MothershipModuleAction> actionId
		) {
			return await _gameService.TriggerAction( gameId, mothershipId, moduleId, actionId );
		}

		public async Task<Mission?> GetMission(
			Id<Game> gameId
		) {
			return await _gameService.GetMission( gameId );
		}

		public async Task<Mission> AddCrewToMission(
			Id<Game> gameId,
			Id<Mission> missionId,
			IEnumerable<Id<Actor>> crew
		) {
			return await _gameService.AddCrewToMission( gameId, missionId, crew );
		}

		public async Task<EncounterCard> GetEncounterCard(
			Id<Game> gameId,
			Id<Mission> missionId
		) {
			Mission? mission = await _gameService.GetMission( gameId );
			if (mission is null) {
				throw new InvalidOperationException();
			}

			EncounterCard card = EncounterCard.GetById( mission.EncounterCardId );
			return card;
		}

		public async Task ResolveEncounter(
			Id<Game> gameId,
			Id<Mission> missionId,
			Id<EncounterCard> encounterCardId,
			Id<EncounterInteraction> encounterInteractionId
		) {
			Mission? mission = await _gameService.GetMission( gameId );
			if (mission is null) {
				throw new InvalidOperationException();
			}

			IEnumerable<Actor> crew = await _gameService.GetCrew( gameId );
			int magnitude = 0;
			EncounterCard encounterCard = EncounterCard.GetById( encounterCardId );
			EncounterInteraction interaction = encounterCard.Interactions.First( i => i.Id.Equals( encounterInteractionId ) );
			if( interaction.Outcomes.RoleFocusCheck != RoleFocusCheck.None ) {
				// Determine who is to make the check and get their skill deck
				Role targetRole = interaction.Outcomes.RoleFocusCheck.Role;
				Actor targetCrew = crew.First( c => c.Role == targetRole );
				SkillDeck skillDeck = await _gameService.GetSkillDeck( missionId, targetCrew.Id );

				// Perform the check and note success or failure
				FocusCheck focusCheck = interaction.Outcomes.RoleFocusCheck.FocusCheck;
				if( skillDeck.CheckFocus( focusCheck.Focus, targetCrew.Training ) >= focusCheck.Target ) {
					magnitude = interaction.Outcomes.Success;
				} else {
					magnitude = interaction.Outcomes.Failure;
				}
				await _gameService.UpdateSkillDeck( missionId, targetCrew.Id, skillDeck );
			} else {
				// No skill required so automatic success
				magnitude = interaction.Outcomes.Success;
			}

			EncounterOutcomeCard outcome = EncounterOutcomeCard.GetById( mission.EncounterOutcomeCardId );
			EncounterOutcome result = outcome.GetResult( magnitude );
			//TODO: Handle the result here
		}

		private ClientPlayer ToClientPlayer( Player player ) {
			return new ClientPlayer(
				new Id<ClientPlayer>( player.Id.Value ),
				player.GameId,
				player.Name );
		}
	}
}
