using NUnit.Framework;

namespace RiftDrive.Shared.Message.Tests {
	[TestFixture]
	public class AuthorizationTokenTests {

		[Test]
		public void Ctor_ValidProperties_PropertiesSet() {
			var token = new AuthorizationToken( "id", "access", "type", 1, "refresh" );

			Assert.AreEqual( "id", token.id_token );
			Assert.AreEqual( "access", token.access_token );
			Assert.AreEqual( "type", token.token_type );
			Assert.AreEqual( 1, token.expires_in );
			Assert.AreEqual( "refresh", token.refresh_token );
		}
	}
}
