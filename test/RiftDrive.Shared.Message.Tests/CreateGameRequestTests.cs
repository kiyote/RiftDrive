using NUnit.Framework;

namespace RiftDrive.Shared.Message.Tests {
	[TestFixture]
	public sealed class CreateGameRequestTests {

		[Test]
		public void Ctor_ValidProperties_PropertiesSet() {
			var request = new CreateGameRequest( "game", "player" );

			Assert.AreEqual( "game", request.GameName );
			Assert.AreEqual( "player", request.PlayerName );
		}
	}
}
