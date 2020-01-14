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
using System.Threading.Tasks;
using RiftDrive.Shared.Model;
using RiftDrive.Shared.Model.Client;

namespace RiftDrive.Client.Action {
	public interface IDispatch {

		Task LoadCurrentGame( Id<Game> gameId );

		Task LoadCurrentMission( Id<Game> gameId );

		Task TriggerModuleAction( Id<Game> gameId, Id<Mothership> mothershipId, Id<MothershipModule> moduleId, Id<MothershipModuleAction> actionId );

		Task SelectMissionCrew( Id<Game> gameId, Id<Mission> missionId, IEnumerable<Actor> crew );

		Task LogOut();

		Task UpdateTokens( string idToken, string refreshToken, DateTime tokensExpireAt );

		Task CreateGame( string gameName, string playerName );

		Task LoadGames( Id<ClientUser> userId );

		Task DeleteGame( Id<Game> gameId );

		Task LoadProfile( Id<ClientUser> userId );

		Task UpdateProfileAvatar( string mimeType, string content );

		Task LoadUserInformation();

		Task RecordLogin();

		Task ResolveEncounterCard(
			Id<Game> gameId,
			Id<Mission> missionId,
			Id<EncounterCard> encounterCardId,
			Id<EncounterInteraction> encounterInteractionId
		);
	}
}
