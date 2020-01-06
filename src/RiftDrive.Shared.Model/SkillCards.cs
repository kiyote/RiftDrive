using System;

namespace RiftDrive.Shared.Model {
	public sealed partial class SkillCard {

		public static SkillCard Hesitation = new SkillCard(
			new Id<SkillCard>( "fc6cc119f8a44b0c964f3fa82e106579" ),
			new FocusValue( Focus.None, 0 )
		);

		public static SkillCard Command1 = new SkillCard(
			new Id<SkillCard>( "ce26a17fd0b04e749c54381a102322bf" ),
			new FocusValue( Focus.Command, 1 )
		);

		public static SkillCard Command2 = new SkillCard(
			new Id<SkillCard>( "1cc681d6f06e49b786c7bf8317d39a48" ),
			new FocusValue( Focus.Command, 2 )
		);

		public static SkillCard Command3 = new SkillCard(
			new Id<SkillCard>( "b0edf0f8272f439281c4d37c3598eacb" ),
			new FocusValue( Focus.Command, 3 )
		);

		public static SkillCard Science1 = new SkillCard(
			new Id<SkillCard>( "ae11c8cef15743b2ae438c069e392660" ),
			new FocusValue( Focus.Science, 1 )
		);

		public static SkillCard Engineering1 = new SkillCard(
			new Id<SkillCard>( "97d8e017b1c3479fa6b807d32590d4a0" ),
			new FocusValue( Focus.Engineering, 1 )
		);

		public static SkillCard Security1 = new SkillCard(
			new Id<SkillCard>( "8a8b28e85e6b4d24990bd0a1b7d589e5" ),
			new FocusValue( Focus.Security, 1 )
		);
	}
}
