using System;
using System.Collections.Generic;
using RiftDrive.Common.Model;

namespace RiftDrive.Server.Hubs {
	public class HubGroupManager {
		public const string GameManagementGroupId = "8689305bd60f4745a82bf4ca35e9d99f";

		private readonly List<HubGroup> _groups;

		public HubGroupManager() {
			_groups = new List<HubGroup>();
			_groups.Add( new HubGroup( GameManagementGroupId, "Game Mangement Group" ) );
		}

		public IEnumerable<HubGroup> Groups => _groups;
	}
}
