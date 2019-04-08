using NUnit.Framework;

namespace RiftDrive.Shared.Provider.Tests {
	public class NameProviderTests {

		private INameProvider _nameProvider;

		[OneTimeSetUp]
		public void OneTimeSetup() {
			_nameProvider = new NameProvider( new RandomProvider() );
		}

		[Test]
		public void CreateMothershipName_ValidRandom_NameGenerated() {
			string actual = _nameProvider.CreateMothershipName();

			Assert.IsFalse( string.IsNullOrWhiteSpace( actual ) );
		}
	}
}
