using System;
using System.Collections.Generic;

namespace RiftDrive.Shared.Model {
	public sealed class Skill : IEquatable<Skill> {

		public Skill(
			Id<Skill> id,
			string name,
			IEnumerable<Role> allowedRoles
		) {
			Id = id;
			Name = name;
			AllowedRoles = allowedRoles;
		}

		public Id<Skill> Id { get; }

		public string Name { get; }

		public IEnumerable<Role> AllowedRoles { get; }

		public bool Equals( Skill other ) {
			if (ReferenceEquals( other, this )) {
				return false;
			}

			return Id.Equals( other.Id )
				&& string.Equals( Name, other.Name, StringComparison.Ordinal )
				&& AllowedRoles.Similar( other.AllowedRoles );
		}

		public override bool Equals( object obj ) {
			if( !( obj is Skill target ) ) {
				return false;
			}

			return Equals( target as Skill );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + Id.GetHashCode();
				result = ( result * 31 ) + Name.GetHashCode();
				result = ( result * 31 ) + AllowedRoles.GetFinalHashCode();

				return result;
			}
		}
	}
}
