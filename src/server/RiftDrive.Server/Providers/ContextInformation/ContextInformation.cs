using System;
using RiftDrive.Common.Model;

namespace RiftDrive.Server.Providers.ContextInformation {
	internal sealed class ContextInformation : IContextInformation {

		private readonly Id<Identification> _identificationId;
		private readonly User _user;

		public ContextInformation(
			Id<Identification> identificationId,
			User user
		) {
			_identificationId = identificationId;
			_user = user;
		}

		Id<Identification> IContextInformation.IdentificationId => _identificationId;

		User IContextInformation.User => _user;
	}
}
