using System;
using System.Diagnostics.CodeAnalysis;
using RiftDrive.Client.Services.Identification;
using RiftDrive.Common.Model;

namespace RiftDrive.Client.Actions {

	[SuppressMessage( "Performance", "CA1812", Justification = "Dependency Injection" )]
	internal class IdentificationState : IIdentificationState, IIdentificationStateMutator {

		private User _user;
		private Tokens _tokens;

		User IIdentificationState.User => _user;

		Tokens IIdentificationState.Tokens => _tokens;

		bool IIdentificationState.IsAuthenticated => ( _user != default );

		void IIdentificationStateMutator.UpdateTokens( Tokens tokens ) {
			_tokens = tokens;
		}

		void IIdentificationStateMutator.UpdateUser( User user ) {
			_user = user;
		}
	}
}
