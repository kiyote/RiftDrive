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
using RiftDrive.Shared.Model;
using RiftDrive.Server.Model;
using System.Threading.Tasks;

namespace RiftDrive.Server.Repository {
	public interface IPlayerRepository {
		Task<Player> Create( Id<Game> gameId, Id<Player> playerId, Id<User> userId, string name, DateTime createdOn );

		Task Delete( Id<Game> gameId, Id<Player> playerId );

		Task<Player?> Get( Id<Game> gameId, Id<Player> playerId );

		Task<IEnumerable<Player>> GetPlayers( Id<Game> gameId );
	}
}
