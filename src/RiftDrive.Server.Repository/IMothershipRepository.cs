using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RiftDrive.Server.Model;
using RiftDrive.Shared;

namespace RiftDrive.Server.Repository {
	public interface IMothershipRepository {
		Task<Mothership> Create( Id<Game> gameId, Id<Mothership> mothershipId, string name, DateTime createdOn );
	}
}
