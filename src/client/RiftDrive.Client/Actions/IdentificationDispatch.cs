using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using RiftDrive.Client.Hubs;
using RiftDrive.Client.Providers;
using RiftDrive.Client.Services.Identification;
using RiftDrive.Common.Model;

namespace RiftDrive.Client.Actions {

	[SuppressMessage( "Performance", "CA1812", Justification = "Dependency Injection" )]
	internal sealed class IdentificationDispatch : IIdentificationDispatch {

		private readonly IIdentificationService _identificationService;
		private readonly IIdentificationStateProvider _identificationStateProvider;
		private readonly IIdentificationStateMutator _identificationStateMutator;
		private readonly IStateMonitor _appState;
		private readonly IClientHub _clientHub;

		public IdentificationDispatch(
			IIdentificationService identificationService,
			IClientHub clientHub,
			IIdentificationStateProvider identificationStateProvider,
			IIdentificationStateMutator identificationStateMutator,
			IStateMonitor appState
		) {
			_identificationService = identificationService;
			_clientHub = clientHub;
			_identificationStateProvider = identificationStateProvider;
			_identificationStateMutator = identificationStateMutator;
			_appState = appState;
		}

		async Task IIdentificationDispatch.InitializeAsync() {
			Tokens tokens = await _identificationStateProvider.GetTokensAsync().ConfigureAwait( false );
			User user = await _identificationStateProvider.GetUserAsync().ConfigureAwait( false );
			_identificationStateMutator.UpdateUser( user );
			_identificationStateMutator.UpdateTokens( tokens );
		}

		async Task IIdentificationDispatch.GetTokens( string code ) {
			Tokens tokens = await _identificationService.GetTokensAsync( code ).ConfigureAwait( false );
			await _identificationStateProvider.SetTokensAsync( tokens ).ConfigureAwait( false );
		}

		async Task IIdentificationDispatch.RecordLogin() {
			Tokens tokens = await _identificationStateProvider.GetTokensAsync().ConfigureAwait( false );
			User user = await _identificationService.RecordLogin( tokens.id_token ).ConfigureAwait( false );
			await _identificationStateProvider.SetUserAsync( user ).ConfigureAwait( false );
			_identificationStateMutator.UpdateUser( user );
			_appState.FireOnStateChanged();
		}

		async Task IIdentificationDispatch.CompleteLogin() {
			await _clientHub.HackCreateConnection();
			await _clientHub.ConnectAsync();
		}
	}
}
