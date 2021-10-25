using System;
using RiftDrive.Common.Model;

namespace RiftDrive.Server.Providers.ContextInformation {
	public interface IContextInformation {
		Id<Identification> IdentificationId { get; }

		User User { get; }
	}
}
