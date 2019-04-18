using System;

namespace RiftDrive.Client.State {
	public interface IAuthenticationState {
		string AccessToken { get; }

		string Name { get; }

		string RefreshToken { get; }

		DateTime TokensExpireAt { get; }

		string Username { get; }

		bool IsAuthenticated { get; }
	}
}
