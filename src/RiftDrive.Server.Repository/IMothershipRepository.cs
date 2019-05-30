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

namespace RiftDrive.Server.Repository {
	public interface IMothershipRepository {
		Task<Mothership> CreateMothership( Id<Game> gameId, Id<Mothership> mothershipId, string name, int availableCrew, int remainingFuel, DateTime createdOn );

		Task DeleteMothership( Id<Game> gameId );

		Task<MothershipAttachedModule> CreateModule( Id<Mothership> mothershipId, Id<MothershipModule> mothershipModuleId, int remainingPower, DateTime createdOn );

		Task<Mothership?> GetMothership( Id<Game> gameId );

		Task<Mothership?> GetMothership( Id<Game> gameId, Id<Mothership> mothershipId );

		Task<IEnumerable<MothershipAttachedModule>> GetAttachedModules( Id<Game> gameId, Id<Mothership> mothershipId );

		Task<Mothership> SetAvailableCrew( Id<Game> gameId, Id<Mothership> mothershipId, int availableCrew );

		Task<Mothership> SetRemainingFuel( Id<Game> gameId, Id<Mothership> mothershipId, int remainingFuel );

		Task<MothershipAttachedModule> GetAttachedModule( Id<Game> gameId, Id<Mothership> mothershipId, Id<MothershipModule> moduleId );

		Task<MothershipAttachedModule> SetRemainingPower( Id<Game> gameId, Id<Mothership> mothershipId, Id<MothershipModule> moduleId, int remainingPower );
	}
}
