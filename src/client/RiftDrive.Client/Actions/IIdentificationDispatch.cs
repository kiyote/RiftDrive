using System;
using System.Threading.Tasks;

namespace RiftDrive.Client.Actions {
	public interface IIdentificationDispatch {

		Task InitializeAsync();

		Task GetTokens( string code );

		Task RecordLogin();

		Task CompleteLogin();
	}
}
