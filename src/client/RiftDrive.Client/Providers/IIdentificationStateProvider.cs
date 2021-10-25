using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiftDrive.Client.Services.Identification;
using RiftDrive.Common.Model;

namespace RiftDrive.Client.Providers {
	public interface IIdentificationStateProvider {

		Task SetTokensAsync( Tokens tokens );

		Task<Tokens> GetTokensAsync();

		Task SetUserAsync( User user );

		Task<User> GetUserAsync();
	}
}
