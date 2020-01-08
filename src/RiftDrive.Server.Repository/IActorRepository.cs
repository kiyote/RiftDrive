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

namespace RiftDrive.Server.Repository {
	public interface IActorRepository {
		Task<Actor> Create(
			Id<Game> gameId,
			Id<Actor> actorId,
			string name,
			Role role,
			int discipline,
			int talent,
			int training,
			IEnumerable<Skill> skills,
			DateTime createdOn );

		Task<IEnumerable<Actor>> GetActors( Id<Game> gameId );

		Task<Actor?> GetActor( Id<Game> gameId, Id<Actor> actorId );

		Task Delete( Id<Game> gameId, Id<Actor> actorId );

		Task<IEnumerable<Skill>> GetActorSkills( Id<Game> gameId, Id<Actor> actorId );
	}
}
