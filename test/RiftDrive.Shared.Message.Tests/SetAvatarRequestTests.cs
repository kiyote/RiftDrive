using NUnit.Framework;

namespace RiftDrive.Shared.Message.Tests {

	[TestFixture]
	public sealed class SetAvatarRequestTests {

		[Test]
		public void Ctor_ValidParameters_ParametersSet() {
			var request = new SetAvatarRequest( "type", "content" );

			Assert.AreEqual( "type", request.ContentType );
			Assert.AreEqual( "content", request.Content );
		}
	}
}
