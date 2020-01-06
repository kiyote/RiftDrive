using NUnit.Framework;

namespace RiftDrive.Shared.Message.Tests {

	[TestFixture]
	public sealed class StartGameRequestTests {

		[Test]
		public void Ctor_ValidParameters_PropertiesSet() {
			var request = new StartGameRequest( "message" );

			Assert.AreEqual( "message", request.Message );
		}
	}
}
