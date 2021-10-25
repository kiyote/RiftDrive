using System;
using System.Threading.Tasks;
using RiftDrive.Common.Model;

namespace RiftDrive.Server.Services.Bouncer {
	public interface IBouncerService {
		Task<Identification> GetIdentificationAsync( Id<Identification> id );

		Task<User> GetUserAsync( Id<Identification> id );

		Task<User> RecordLoginAsync( Id<Identification> id );
	}
}
