using System;
using System.Threading.Tasks;
using RiftDrive.Common.Model;

namespace RiftDrive.Server.Services.Bouncer {
	public interface IUserManager {
		Task<User> GetUserAsync( Id<Identification> identificationId );

		Task<User> SetLastLoginAsync(
			Id<User> userId,
			DateTime lastLogin );

		Task<User> AddUserAsync(
			Id<User> userId,
			string username,
			DateTime createdOn,
			DateTime lastLogin );

		Task AddUserIdentification(
			Id<Identification> identificationId,
			Id<User> userId,
			DateTime createdOn );
	}
}
