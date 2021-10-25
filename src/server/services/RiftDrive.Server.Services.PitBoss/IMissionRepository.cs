using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RiftDrive.Common.Model;

namespace RiftDrive.Server.Services.PitBoss {
	internal interface IMissionRepository {
		Task DeleteMissionsAsync( Id<Game> gameId );
	}
}
