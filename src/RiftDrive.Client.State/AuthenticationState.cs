using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiftDrive.Client.State {
	internal sealed class AuthenticationState : IAuthenticationState {

		public AuthenticationState() {
			Username = "";
			Name = "";
			AccessToken = "";
			RefreshToken = "";
			TokensExpireAt = DateTime.MinValue.ToUniversalTime();
		}

		public AuthenticationState(
			IAuthenticationState state,
			string accessToken,
			string refreshToken,
			DateTime tokensExpireAt
		) {
			Username = state.Username;
			Name = state.Name;
			AccessToken = accessToken;
			RefreshToken = refreshToken;
			TokensExpireAt = tokensExpireAt;
		}

        public AuthenticationState(
            IAuthenticationState state,
            string username,
            string name
        )
        {
            Username = username;
            Name = name;
            AccessToken = state.AccessToken;
            RefreshToken = state.RefreshToken;
            TokensExpireAt = state.TokensExpireAt;
        }

        public AuthenticationState(
            string username,
			string name,
            string accessToken,
            string refreshToken,
            DateTime tokensExpireAt
        )
        {
            Username = username;
            Name = name;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            TokensExpireAt = tokensExpireAt;
        }

		public string Username { get; }

		public string Name { get; }

		public string AccessToken { get; }

		public string RefreshToken { get; }

		public DateTime TokensExpireAt { get; }

		public bool IsAuthenticated {
			get {
				return ( TokensExpireAt >= DateTime.UtcNow );
			}
		}

		public static async Task<AuthenticationState> InitialState( IStateStorage storage ) {
			var accessToken = await storage.GetAsString( "AccessToken" );
			var refreshToken = await storage.GetAsString( "RefreshToken" );
			var tokensExpireAt = await storage.GetAsDateTime( "TokensExpireAt" );
			var name = await storage.GetAsString( "Name" );
			var username = await storage.GetAsString( "Username" );

			return new AuthenticationState( username, name, accessToken, refreshToken, tokensExpireAt );
		}
	}
}
