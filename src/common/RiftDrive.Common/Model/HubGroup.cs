using System;
using System.Collections.Generic;
using System.Text;

namespace RiftDrive.Common.Model {
	public sealed class HubGroup {

		public HubGroup(
			string id,
			string name
		) {
			Id = id;
			Name = name;
		}

		public string Id { get; }

		public string Name { get; }
	}
}
