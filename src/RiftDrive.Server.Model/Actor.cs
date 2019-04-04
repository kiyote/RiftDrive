using System;
using RiftDrive.Shared;

namespace RiftDrive.Server.Model {
	public sealed class Actor: IEquatable<Actor> {

		public Actor(
			Id<Actor> id,
			Id<Game> gameId,
			string name
		) {
			Id = id;
			GameId = gameId;
			Name = name;
		}

		public Id<Actor> Id { get; }

		public Id<Game> GameId { get; }

		public string Name { get; }

		/// <summary>
		/// The number of cards you can draw.
		/// </summary>
		public int Intelligence { get; }

		/// <summary>
		/// The number of cards you can hold.
		/// </summary>
		public int Talent { get; }

		/// <summary>
		/// The number of cards you can play.
		/// </summary>
		public int Training { get; }

		public bool Equals( Actor other ) {
			if (other is null ) {
				return false;
			}

			if (ReferenceEquals(other, this)) {
				return true;
			}

			return Id.Equals( other.Id )
				&& GameId.Equals( other.GameId )
				&& string.Equals( Name, other.Name, StringComparison.Ordinal );
		}

		public override bool Equals( object obj ) {
			return Equals( obj as Actor );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + Id.GetHashCode();
				result = ( result * 31 ) + GameId.GetHashCode();
				result = ( result * 31 ) + Name.GetHashCode();

				return result;
			}
		}
	}
}
