using System.Collections.Generic;

namespace RiftDrive.Shared.Model {
	public sealed partial class SkillDeck {

		public static SkillDeck Command = new SkillDeck(
			new Id<SkillDeck>( "51786b69bbde4fd482e26dfab968f482" ),
			new List<SkillCard>() {			
				SkillCard.Hesitation,
				SkillCard.Hesitation,
				SkillCard.Hesitation,
				SkillCard.Command1,
				SkillCard.Command1,
				SkillCard.Command1,
				SkillCard.Command1,
				SkillCard.Command1,
				SkillCard.Command1,
				SkillCard.Command1
			}
		);

		public static SkillDeck Science = new SkillDeck(
			new Id<SkillDeck>( "a36fad9838384afe9e238c994e482d49" ),
			new List<SkillCard>() {
				SkillCard.Hesitation,
				SkillCard.Hesitation,
				SkillCard.Hesitation,
				SkillCard.Science1,
				SkillCard.Science1,
				SkillCard.Science1,
				SkillCard.Science1,
				SkillCard.Science1,
				SkillCard.Science1,
				SkillCard.Science1
			}
		);

		public static SkillDeck Engineering = new SkillDeck(
			new Id<SkillDeck>( "77d18b9d2ff54fc8b7c928c829f31f91" ),
			new List<SkillCard>() {
				SkillCard.Hesitation,
				SkillCard.Hesitation,
				SkillCard.Hesitation,
				SkillCard.Engineering1,
				SkillCard.Engineering1,
				SkillCard.Engineering1,
				SkillCard.Engineering1,
				SkillCard.Engineering1,
				SkillCard.Engineering1,
				SkillCard.Engineering1
			}
		);

		public static SkillDeck Security = new SkillDeck(
			new Id<SkillDeck>( "fa71b8ce820f462d9b7ef6efe188e583" ),
			new List<SkillCard>() {
				SkillCard.Hesitation,
				SkillCard.Hesitation,
				SkillCard.Hesitation,
				SkillCard.Security1,
				SkillCard.Security1,
				SkillCard.Security1,
				SkillCard.Security1,
				SkillCard.Security1,
				SkillCard.Security1,
				SkillCard.Security1
			}
		);
	}
}
