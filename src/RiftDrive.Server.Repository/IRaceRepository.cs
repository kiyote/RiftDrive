using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RiftDrive.Server.Model;
using RiftDrive.Shared;

namespace RiftDrive.Server.Repository {
	public interface IRaceRepository {
		Task<Race> Get( Id<Race> raceId );
	}
}
