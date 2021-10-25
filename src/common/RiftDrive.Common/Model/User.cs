using System;

namespace RiftDrive.Common.Model {
	public sealed class User : IEquatable<User> {

		public User(
			Id<User> id,
			string avatarUrl,
			DateTime lastLogin,
			DateTime? previousLogin,
			DateTime createdOn,
			string name
		) {
			Id = id;
			AvatarUrl = avatarUrl;
			CreatedOn = createdOn;
			LastLogin = lastLogin.ToUniversalTime();
			PreviousLogin = previousLogin?.ToUniversalTime();
			Name = name;
		}

		public Id<User> Id { get; }

		public string AvatarUrl { get; }

		public DateTime LastLogin { get; }

		public DateTime? PreviousLogin { get; }

		public string Name { get; }

		public DateTime CreatedOn { get; }

		public bool Equals( User other ) {
			if( other is null ) {
				return false;
			}

			if( ReferenceEquals( other, this ) ) {
				return true;
			}

			return Id.Equals( other.Id )
				&& string.Equals( AvatarUrl, other.AvatarUrl, StringComparison.Ordinal )
				&& LastLogin.Nearly( other.LastLogin )
				&& PreviousLogin.Nearly( other.PreviousLogin )
				&& CreatedOn.Nearly( other.CreatedOn )
				&& string.Equals( Name, other.Name, StringComparison.Ordinal );
		}

		public override bool Equals( object obj ) {
			if( !( obj is User target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			return HashCode.Combine( Id, AvatarUrl, LastLogin, PreviousLogin, CreatedOn, Name );
		}
	}

}
