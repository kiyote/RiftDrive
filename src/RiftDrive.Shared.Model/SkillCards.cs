using System;
using System.Collections.Generic;
using System.Text;

namespace RiftDrive.Shared.Model {
	public sealed partial class SkillCard {

		public static SkillCard Command1 = new SkillCard( new Id<SkillCard>( "ce26a17fd0b04e749c54381a102322bf" ), new SkillValue( Skill.Command, 1 ) );
		public static SkillCard Command2 = new SkillCard( new Id<SkillCard>( "1cc681d6f06e49b786c7bf8317d39a48" ), new SkillValue( Skill.Command, 2 ) );
		public static SkillCard Command3 = new SkillCard( new Id<SkillCard>( "b0edf0f8272f439281c4d37c3598eacb" ), new SkillValue( Skill.Command, 3 ) );
	}
}
