using System;
using System.Collections.Generic;

namespace RiftDrive.Shared.Model {
	public sealed partial class Skill : IEquatable<Skill> {

		public Skill(
			Id<Skill> id,
			string name,
			IEnumerable<Role> allowedRoles,
			Id<SkillDeck> deckId
		) {
			Id = id;
			Name = name;
			AllowedRoles = allowedRoles;
			DeckId = deckId;
		}

		public Id<Skill> Id { get; }

		public string Name { get; }

		public IEnumerable<Role> AllowedRoles { get; }

		public Id<SkillDeck> DeckId { get; }

		public bool Equals( Skill other ) {
			if (ReferenceEquals( other, this )) {
				return false;
			}

			return Id.Equals( other.Id )
				&& string.Equals( Name, other.Name, StringComparison.Ordinal )
				&& AllowedRoles.Similar( other.AllowedRoles )
				&& DeckId.Equals( other.DeckId );
		}

		public override bool Equals( object obj ) {
			if( !( obj is Skill target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + Id.GetHashCode();
				result = ( result * 31 ) + Name.GetHashCode();
				result = ( result * 31 ) + AllowedRoles.GetFinalHashCode();
				result = ( result * 31 ) + DeckId.GetHashCode();

				return result;
			}
		}
	}
}
