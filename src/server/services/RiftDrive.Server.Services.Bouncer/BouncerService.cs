using System;
using System.Threading.Tasks;
using RiftDrive.Common.Model;

namespace RiftDrive.Server.Services.Bouncer {
	internal sealed class BouncerService : IBouncerService {

		private readonly IIdentificationManager _identificationManager;
		private readonly IUserManager _userManager;

		public BouncerService(
			IIdentificationManager identificationManager,
			IUserManager userManager
		) {
			_identificationManager = identificationManager;
			_userManager = userManager;
		}

		async Task<Identification> IBouncerService.GetIdentificationAsync( Id<Identification> id ) {
			return await _identificationManager.GetIdentificationAsync( id );
		}

		async Task<User> IBouncerService.GetUserAsync( Id<Identification> id ) {
			return await _userManager.GetUserAsync( id );
		}

		async Task<User> IBouncerService.RecordLoginAsync( Id<Identification> id ) {
			User user = await _userManager.GetUserAsync( id );

			if (user == default) {
				Identification identification = await _identificationManager.GetIdentificationAsync( id );
				user = await _userManager.AddUserAsync(
					new Id<User>(),
					identification.Name,
					DateTime.UtcNow,
					DateTime.UtcNow );

				await _userManager.AddUserIdentification( id, user.Id, DateTime.UtcNow );
			} else {
				user = await _userManager.SetLastLoginAsync( user.Id, DateTime.UtcNow );
			}

			return user;
		}
	}
}
