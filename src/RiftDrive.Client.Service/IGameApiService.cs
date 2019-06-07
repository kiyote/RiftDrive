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
using System.Collections.Generic;
using System.Threading.Tasks;
using RiftDrive.Shared.Model;

namespace RiftDrive.Client.Service {
	public interface IGameApiService {
		Task<Game> CreateGame( string gameName, string playerName );

		Task DeleteGame( Id<Game> gameId );

		Task<Game> StartGame( Id<Game> gameId, string message );

		Task<IEnumerable<Game>> GetGames();

		Task<IEnumerable<ClientPlayer>> GetPlayers( Id<Game> gameId );

		Task<Game?> GetGame( Id<Game> gameId );

		Task<Mothership?> GetMothership( Id<Game> gameId );

		Task<IEnumerable<Actor>> GetCrew( Id<Game> gameId );

		Task<IEnumerable<MothershipAttachedModule>> GetMothershipModules( Id<Game> gameId, Id<Mothership> mothershipId );

		Task<IEnumerable<string>> TriggerAction(
			Id<Game> gameId,
			Id<Mothership> mothershipId,
			Id<MothershipModule> mothershipModuleId,
			Id<MothershipModuleAction> actionId );

		Task<Mission?> GetMission( Id<Game> gameId );

		Task<Mission> SelectMissionCrew( Id<Game> gameId, Id<Mission> missionId, IEnumerable<Id<Actor>> crew );

		Task<EncounterCard> DrawEncounterCard( Id<Game> gameId, Id<Mission> missionId );
	}
}
