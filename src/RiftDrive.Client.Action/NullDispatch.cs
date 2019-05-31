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
using RiftDrive.Shared.Model;

namespace RiftDrive.Client.Action {
	public sealed class NullDispatch : IDispatch {

		public static IDispatch Instance = new NullDispatch();

		Task IDispatch.CreateGame( string gameName, string playerName ) {
			throw new NotImplementedException();
		}

		Task IDispatch.DeleteGame( Id<Game> gameId ) {
			throw new NotImplementedException();
		}

		Task IDispatch.LoadCurrentGame( Id<Game> gameId ) {
			throw new NotImplementedException();
		}

		Task IDispatch.LoadCurrentMission( Id<Game> gameId ) {
			throw new NotImplementedException();
		}

		Task IDispatch.LoadGames( Id<ClientUser> userId ) {
			throw new NotImplementedException();
		}

		Task IDispatch.LoadProfile( Id<ClientUser> userId ) {
			throw new NotImplementedException();
		}

		Task IDispatch.LoadUserInformation() {
			throw new NotImplementedException();
		}

		Task IDispatch.LogOut() {
			throw new NotImplementedException();
		}

		Task IDispatch.RecordLogin() {
			throw new NotImplementedException();
		}

		Task IDispatch.SelectMissionCrew( Id<Game> gameId, Id<Mission> missionId, IEnumerable<Actor> crew ) {
			throw new NotImplementedException();
		}

		Task IDispatch.TriggerModuleAction( Id<Game> gameId, Id<Mothership> mothershipId, Id<MothershipModule> moduleId, Id<MothershipModuleAction> actionId ) {
			throw new NotImplementedException();
		}

		Task IDispatch.UpdateProfileAvatar( string mimeType, string content ) {
			throw new NotImplementedException();
		}

		Task IDispatch.UpdateTokens( string accessToken, string refreshToken, DateTime tokensExpireAt ) {
			throw new NotImplementedException();
		}
	}
}
