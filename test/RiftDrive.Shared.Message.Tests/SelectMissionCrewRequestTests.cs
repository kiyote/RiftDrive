using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RiftDrive.Shared.Model;

namespace RiftDrive.Shared.Message.Tests {

	[TestFixture]
	public sealed class SelectMissionCrewRequestTests {

		[Test]
		public void Ctor_ValidProperties_PropertiesSet() {
			var id = new Id<Mission>("123");
			var crew = new List<Id<Actor>>() {
			};
			var request = new SelectMissionCrewRequest( id, crew );

			Assert.AreSame( id, request.MissionId );
			Assert.AreSame( crew, request.Crew );
		}
	}
}
