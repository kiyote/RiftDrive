using System;
using System.Threading.Tasks;

namespace RiftDrive.Client.Actions {
	public interface IDispatch {

		Task InitializeAsync();

		IIdentificationDispatch Identification { get; }

		IGameManagementDispatch GameManagement { get; }
	}
}
