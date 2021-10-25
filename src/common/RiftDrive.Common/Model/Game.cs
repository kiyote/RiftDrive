using System;

namespace RiftDrive.Common.Model {
	public sealed class Game: IEquatable<Game> {

		public Game(
			Id<Game> id,
			string name,
			DateTime createdOn
		) {
			if (id == default) {
				throw new ArgumentException( "Id cannot be null", nameof( id ) );
			}
			if (string.IsNullOrWhiteSpace( name )) {
				throw new ArgumentException( "Name cannot be null", nameof( name ) );
			}

			Id = id;
			Name = name;
			CreatedOn = createdOn.ToUniversalTime();
		}

		public Game() {
		}

		public Id<Game> Id { get; set; }

		public string Name { get; set; }

		public DateTime CreatedOn { get; set; }

		public bool Equals( Game other ) {
			if (other is null) {
				return false;
			}

			if (ReferenceEquals( other, this)) {
				return true;
			}

			return Id.Equals( other.Id )
				&& string.Equals( Name, other.Name, StringComparison.Ordinal )
				&& CreatedOn.Nearly( other.CreatedOn );
		}

		public override bool Equals( object obj ) {
			if (!(obj is Game target)) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			return HashCode.Combine( Id, Name, CreatedOn );
		}

		public static bool operator ==(Game a, Game b) {
			return a?.Equals( b ) ?? false;
		}

		public static bool operator !=( Game a, Game b ) {
			return !( a == b );
		}
	}
}
