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
using RiftDrive.Server.Model;
using RiftDrive.Shared.Model;

namespace RiftDrive.Server.Repository {
	public interface IGameRepository {
		Task<Game> Create( Id<Game> gameId, string name, DateTime createdOn );

		Task<IEnumerable<Game>> GetGames( Id<User> userId );

		Task<Game?> GetGame( Id<Game> gameId );

		Task Delete( Id<Game> gameId );

		Task<Game> StartGame( Id<Game> gameId );
	}
}
