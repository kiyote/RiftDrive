using System;
using System.Collections.Generic;
using System.Text;

namespace RiftDrive.Shared.Model {
	public sealed class SkillCard: IEquatable<SkillCard> {

		public SkillCard(
			Id<SkillCard> id,
			Skill skill,
			int magnitude
		) {
			Id = id;
			Skill = skill;
			Magnitude = magnitude;
		}

		public Id<SkillCard> Id { get; set; }

		public Skill Skill { get; set; }

		public int Magnitude { get; set; }

		public bool Equals( SkillCard other ) {
			if (ReferenceEquals(other, this)) {
				return true;
			}

			return Id.Equals( other.Id )
				&& Skill == other.Skill
				&& Magnitude == other.Magnitude;
		}

		public override bool Equals( object obj ) {
			SkillCard? card = obj as SkillCard;

			if (card == default) {
				return false;
			}

			return Equals( card );
		}

		public override int GetHashCode() {
			unchecked {
				var result = Id.GetHashCode();
				result = ( result * 31 ) + Skill.GetHashCode();
				result = ( result * 31 ) + Magnitude.GetHashCode();

				return result;
			}
		}
	}
}
