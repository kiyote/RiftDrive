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
using System.Threading.Tasks;
using System.Linq;
using RiftDrive.Server.Model;
using RiftDrive.Server.Repository;
using RiftDrive.Shared.Model;
using RiftDrive.Shared.Provider;

namespace RiftDrive.Server.Service {
	internal sealed class GameService : IGameService {

		private readonly IGameRepository _gameRepository;
		private readonly IPlayerRepository _playerRepository;
		private readonly IActorRepository _actorRepository;
		private readonly IMothershipRepository _mothershipRepository;
		private readonly INameProvider _nameProvider;
		private readonly IRandomProvider _randomProvider;
		private readonly IMissionRepository _missionRepository;

		public GameService(
			IGameRepository gameRepository,
			IPlayerRepository playerRepository,
			IActorRepository actorRepository,
			IMothershipRepository mothershipRepository,
			INameProvider nameProvider,
			IRandomProvider randomProvider,
			IMissionRepository missionRepository
		) {
			_gameRepository = gameRepository;
			_playerRepository = playerRepository;
			_actorRepository = actorRepository;
			_mothershipRepository = mothershipRepository;
			_nameProvider = nameProvider;
			_randomProvider = randomProvider;
			_missionRepository = missionRepository;
		}

		async Task<IEnumerable<Game>> IGameService.GetGames(
			Id<User> userId
		) {
			return await _gameRepository.GetGames( userId );
		}

		async Task<Game?> IGameService.GetGame(
			Id<Game> gameId
		) {
			return await _gameRepository.GetGame( gameId );
		}

		async Task<Game> IGameService.StartGame(
			Id<Game> gameId
		) {
			return await _gameRepository.StartGame( gameId );
		}

		async Task<Game> IGameService.CreateGame(
			CreateGameConfiguration config
		) {
			Game game = await _gameRepository.Create( new Id<Game>(), config.GameName, config.CreatedOn );
			await _playerRepository.Create( game.Id, new Id<Player>(), config.CreatedBy, config.PlayerName, config.CreatedOn );

			Mothership mothership = await _mothershipRepository.CreateMothership( game.Id, new Id<Mothership>(), _nameProvider.CreateMothershipName(), 4, 10, config.CreatedOn );
			await _mothershipRepository.CreateModule( mothership.Id, MothershipModule.Hanger.Id, 5, config.CreatedOn );
			await _mothershipRepository.CreateModule( mothership.Id, MothershipModule.Cryogenics.Id, 5, config.CreatedOn );

			await _actorRepository.Create( game.Id, new Id<Actor>(), _nameProvider.CreateActorName(), Role.Command, 1, 1, 1, config.CreatedOn );
			await _actorRepository.Create( game.Id, new Id<Actor>(), _nameProvider.CreateActorName(), Role.Engineer, 1, 1, 1, config.CreatedOn );
			await _actorRepository.Create( game.Id, new Id<Actor>(), _nameProvider.CreateActorName(), Role.Science, 1, 1, 1, config.CreatedOn );
			await _actorRepository.Create( game.Id, new Id<Actor>(), _nameProvider.CreateActorName(), Role.Security, 1, 1, 1, config.CreatedOn );

			return game;
		}

		async Task<IEnumerable<Player>> IGameService.GetPlayers(
			Id<Game> gameId
		) {
			return await _playerRepository.GetPlayers( gameId );
		}

		async Task IGameService.DeleteGame(
			Id<Game> gameId
		) {
			IEnumerable<Actor> actors = await _actorRepository.GetActors( gameId );
			foreach( Actor actor in actors ) {
				await _actorRepository.Delete( gameId, actor.Id );
			}
			await _mothershipRepository.DeleteMothership( gameId );
			IEnumerable<Player> players = await _playerRepository.GetPlayers( gameId );
			foreach( Player player in players ) {
				await _playerRepository.Delete( gameId, player.Id );
			}
			// TODO: Delete missions
			await _gameRepository.Delete( gameId );
		}

		async Task<Mothership?> IGameService.GetMothership(
			Id<Game> gameId
		) {
			return await _mothershipRepository.GetMothership( gameId );
		}

		async Task<IEnumerable<Actor>> IGameService.GetCrew(
			Id<Game> gameId
		) {
			return await _actorRepository.GetActors( gameId );
		}

		async Task<IEnumerable<MothershipAttachedModule>> IGameService.GetMothershipModules(
			Id<Game> gameId,
			Id<Mothership> mothershipId
		) {
			return await _mothershipRepository.GetAttachedModules( gameId, mothershipId );
		}

		async Task<Mission?> IGameService.GetMission(
			Id<Game> gameId
		) {
			return await _missionRepository.GetByGameId( gameId );
		}

		async Task<IEnumerable<string>> IGameService.TriggerAction(
			Id<Game> gameId,
			Id<Mothership> mothershipId,
			Id<MothershipModule> moduleId,
			Id<MothershipModuleAction> actionId
		) {
			List<string> result = new List<string>();
			Mothership? mothership = await _mothershipRepository.GetMothership( gameId, mothershipId );

			if( mothership == default ) {
				throw new ArgumentException();
			}

			MothershipAttachedModule module = await _mothershipRepository.GetAttachedModule( gameId, mothershipId, moduleId );
			MothershipModule definition = MothershipModule.GetById( moduleId );
			MothershipModuleAction action = definition.Actions.First( a => a.Id == actionId );
			foreach( MothershipModuleEffect effect in action.Effects ) {
				int magnitude = CalculateMagnitude( effect );
				switch( effect.Effect ) {
					case ModuleEffect.AddCrew: {
							result.Add( magnitude > 0 ? $"Revived {magnitude} additional crew." : "No viable crew found for revival" );
							await _mothershipRepository.SetAvailableCrew( gameId, mothershipId, mothership.AvailableCrew + magnitude );
						}
						break;
					case ModuleEffect.ConsumeFuel: {
							result.Add( $"Consumed {magnitude} fuel." );
							await _mothershipRepository.SetRemainingFuel( gameId, mothershipId, mothership.RemainingFuel - magnitude );
						}
						break;
					case ModuleEffect.ConsumePower: {
							result.Add( $"Consumed {magnitude} power in the {definition.Name} module." );
							await _mothershipRepository.SetRemainingPower( gameId, mothershipId, module.MothershipModuleId, module.RemainingPower - magnitude );
						}
						break;
					case ModuleEffect.ProducePower: {
							IEnumerable<MothershipAttachedModule> modules = await _mothershipRepository.GetAttachedModules( gameId, mothershipId );
							modules = modules.Where( m => m != module );
							foreach( MothershipAttachedModule m in modules ) {
								MothershipModule defn = MothershipModule.GetById( m.MothershipModuleId );
								result.Add( $"Gained {magnitude} power in the {defn.Name} module." );
								await _mothershipRepository.SetRemainingPower( gameId, mothershipId, m.MothershipModuleId, m.RemainingPower + magnitude );
							}
						}
						break;
					case ModuleEffect.LaunchMission: {
							int cardIndex = _randomProvider.Next( EncounterCard.All.Count() );
							EncounterCard card = EncounterCard.All.ElementAt( cardIndex );
							int raceIndex = _randomProvider.Next( Race.All.Count() );
							Race race = Race.All.ElementAt( raceIndex );
							EncounterOutcomeDeck deck = EncounterOutcomeDeck.GetById( race.Id );
							int outcomeIndex = _randomProvider.Next( deck.Cards.Count() );
							EncounterOutcomeCard outcome = deck.Cards.ElementAt( outcomeIndex );

							Mission mission = await _missionRepository.Create( gameId, new Id<Mission>(), card.Id, race.Id, outcome.Id, DateTime.UtcNow, MissionStatus.SelectCrew );

							result.Add( $"Launching mission." );
						}
						break;
				}
			}

			return result;
		}

		async Task<Mission> IGameService.AddCrewToMission(
			Id<Mission> missionId,
			IEnumerable<Id<Actor>> crew
		) {
			return await _missionRepository.AddCrewToMission( missionId, crew, MissionStatus.RaceEncounter );
		}

		Task<EncounterCard> IGameService.GetEncounterCard(
			Id<Game> gameId,
			Id<Mission> missionId
		) {
			int cardIndex = _randomProvider.Next( EncounterCard.All.Count() );
			EncounterCard card = EncounterCard.All.ElementAt( cardIndex );

			return Task.FromResult( card );
		}

		private int CalculateMagnitude( MothershipModuleEffect effect ) {
			if( effect.Magnitude == int.MinValue ) {
				return _randomProvider.Next( effect.RandomMin, effect.RandomMax );
			}

			return effect.Magnitude;
		}

	}
}
