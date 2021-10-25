using System;

namespace RiftDrive.Common.Model {
	public sealed class Identification : IEquatable<Identification> {

		public Identification(
			Id<Identification> id,
			string name,
			string email,
			DateTime createdOn
		) {
			Id = id;
			Name = name;
			Email = email;
			CreatedOn = createdOn.ToUniversalTime();
		}

		public Id<Identification> Id { get; }

		public string Name { get; }

		public string Email { get; }

		public DateTime CreatedOn { get; }

		public bool Equals( Identification other ) {
			if( other is null ) {
				return false;
			}

			if( ReferenceEquals( other, this ) ) {
				return true;
			}

			return Equals( Id, other.Id )
				&& string.Equals( Name, other.Name, StringComparison.Ordinal )
				&& string.Equals( Email, other.Email, StringComparison.Ordinal )
				&& CreatedOn.Nearly( other.CreatedOn );
		}

		public override bool Equals( object obj ) {
			if( !( obj is Identification target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			return HashCode.Combine( Id, Name, Email, CreatedOn );
		}
	}
}
