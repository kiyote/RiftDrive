using System.Collections.Generic;
using NUnit.Framework;

namespace RiftDrive.Shared.Model.Tests {
	[TestFixture]
	public class EncounterCardTests {

		[Test]
		public void Ctor_ValidProperties_PropertiesSet() {
			var id = new Id<EncounterCard>( "123" );
			string description = "description";
			var check = new RoleFocusCheck( Role.Science, new FocusCheck( Focus.Science, 1 ) );
			IEnumerable<EncounterInteraction> interactions = new List<EncounterInteraction>() {
				new EncounterInteraction(
					new Id<EncounterInteraction>(),
					"encounter description",
					new EncounterInteractionCheck(
						new RoleFocusCheck(
							Role.Engineer,
							new FocusCheck(Focus.Engineering, 2)),
						3,
						4))
			};
			var card = new EncounterCard( id, description, check, interactions );

			Assert.AreSame( description, card.Description );
			Assert.AreSame( id, card.Id );
			Assert.AreSame( interactions, card.Interactions );
			Assert.AreSame( check, card.RevealRace );
		}
	}
}
