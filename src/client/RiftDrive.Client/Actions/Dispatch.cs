using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace RiftDrive.Client.Actions {

	[SuppressMessage( "Performance", "CA1812", Justification = "Dependency Injection" )]
	internal sealed class Dispatch : IDispatch {

		private readonly IIdentificationDispatch _identificationDispatch;
		private readonly IGameManagementDispatch _gameManagementDispatch;

		public Dispatch(
			IIdentificationDispatch identificationDispatch,
			IGameManagementDispatch gameManagementDispatch
		) {
			_identificationDispatch = identificationDispatch;
			_gameManagementDispatch = gameManagementDispatch;
		}

		IIdentificationDispatch IDispatch.Identification => _identificationDispatch;

		IGameManagementDispatch IDispatch.GameManagement => _gameManagementDispatch;

		async Task IDispatch.InitializeAsync() {
			await _identificationDispatch.InitializeAsync().ConfigureAwait( false );
			await _gameManagementDispatch.InitializeAsync().ConfigureAwait( false );
		}
	}
}
