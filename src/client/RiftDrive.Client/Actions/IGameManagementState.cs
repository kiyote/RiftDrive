using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using RiftDrive.Common.Model;

namespace RiftDrive.Client.Actions {
	public interface IGameManagementState {
		IEnumerable<Game> AvailableGames { get; }

		IEnumerable<Game> ActiveGames { get; }

		IImmutableDictionary<Id<Game>, Game> Games { get; }
	}
}
