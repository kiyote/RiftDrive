using System;
using RiftDrive.Client.Services.Identification;
using RiftDrive.Common.Model;

namespace RiftDrive.Client.Actions {
	public interface IIdentificationState { 
		User User { get; }

		Tokens Tokens { get; }

		bool IsAuthenticated { get; }
	}
}
