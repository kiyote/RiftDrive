using NUnit.Framework;

namespace RiftDrive.Shared.Message.Tests {
	[TestFixture]
	public sealed class SetAvatarResponseTests {

		[Test]
		public void Ctor_ValidParamater_PropertiesSet() {
			var response = new SetAvatarResponse( "url" );

			Assert.AreEqual( "url", response.Url );
		}
	}
}
