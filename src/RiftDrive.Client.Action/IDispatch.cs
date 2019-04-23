using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RiftDrive.Shared;

namespace RiftDrive.Client.Action {
	public interface IDispatch {
		Task PlayGame( Id<Game> gameId );

		Task ViewGame( Id<Game> gameId );
	}
}
