using System;
using RiftDrive.Client.Services.Identification;
using RiftDrive.Common.Model;

namespace RiftDrive.Client.Actions {
	public interface IIdentificationStateMutator {
		void UpdateUser( User user );

		void UpdateTokens( Tokens tokens );
	}
}
