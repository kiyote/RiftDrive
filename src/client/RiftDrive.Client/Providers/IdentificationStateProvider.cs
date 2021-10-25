using System;
using System.Threading.Tasks;
using RiftDrive.Client.Services.Identification;
using RiftDrive.Common.Model;

namespace RiftDrive.Client.Providers {
	public class IdentificationStateProvider : IIdentificationStateProvider {

		private readonly IStateProvider _state;

		public IdentificationStateProvider(
			IStateProvider stateProvider
		) {
			_state = stateProvider;
		}

		async Task<Tokens> IIdentificationStateProvider.GetTokensAsync() {
			return await _state.Get<Tokens>( "Identification::Tokens" );
		}

		async Task<User> IIdentificationStateProvider.GetUserAsync() {
			return await _state.Get<User>( "Identification::User" );
		}

		async Task IIdentificationStateProvider.SetTokensAsync( Tokens tokens ) {
			await _state.Set( "Identification::Tokens", tokens );
		}

		async Task IIdentificationStateProvider.SetUserAsync( User user ) {
			await _state.Set( "Identification::User", user );
		}
	}
}
