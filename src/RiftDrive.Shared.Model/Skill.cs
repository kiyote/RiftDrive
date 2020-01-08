using System;
using System.Collections.Generic;

namespace RiftDrive.Shared.Model {
	public sealed partial class Skill : IEquatable<Skill> {

		public Skill(
			Id<Skill> id,
			string name,
			IEnumerable<Role> allowedRoles,
			Id<SkillCardPack> packId
		) {
			Id = id;
			Name = name;
			AllowedRoles = allowedRoles;
			PackId = packId;
		}

		public Id<Skill> Id { get; }

		public string Name { get; }

		public IEnumerable<Role> AllowedRoles { get; }

		public Id<SkillCardPack> PackId { get; }

		public bool Equals( Skill other ) {
			if (ReferenceEquals( other, this )) {
				return true;
			}

			return Id.Equals( other.Id )
				&& string.Equals( Name, other.Name, StringComparison.Ordinal )
				&& AllowedRoles.Similar( other.AllowedRoles )
				&& PackId.Equals( other.PackId );
		}

		public override bool Equals( object obj ) {
			if( !( obj is Skill target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			return HashCode.Combine( Id, Name, AllowedRoles, PackId );
		}
	}
}
