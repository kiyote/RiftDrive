using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider.Model;
using RiftDrive.Common.Model;
using RiftDrive.Server.Services.AWS;
using Microsoft.Extensions.Options;

namespace RiftDrive.Server.Services.Bouncer {
	internal sealed class IdentificationManager: IIdentificationManager {

		private readonly CognitoContext<IdentificationManager> _context;
		private readonly IdentificationOptions _options;

		public IdentificationManager(
			CognitoContext<IdentificationManager> context,
			IOptions<IdentificationOptions> options
		) {
			_options = options.Value;
			_context = context;
		}

		async Task<Identification> IIdentificationManager.GetIdentificationAsync( Id<Identification> id ) {
			ListUsersResponse response = await _context.Provider.ListUsersAsync( new ListUsersRequest() {
				Filter = $"sub = \"{id.Value}\"",
				UserPoolId = _options.UserPoolId,
				AttributesToGet = new List<string>() {
					"email",
					"name"
				}
			} ).ConfigureAwait( false );

			UserType userType = response.Users.FirstOrDefault();
			if( userType == default ) {
				return default;
			}

			return new Identification(
				id,
				userType.Attributes.FirstOrDefault( a => string.Equals( a.Name, "name", StringComparison.OrdinalIgnoreCase ) )?.Value ?? string.Empty,
				userType.Attributes.FirstOrDefault( a => string.Equals( a.Name, "email", StringComparison.OrdinalIgnoreCase ) )?.Value ?? string.Empty,
				userType.UserCreateDate				
			);
		}
	}
}
