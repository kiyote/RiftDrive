using System.Threading.Tasks;
using RiftDrive.Common.Model;

namespace RiftDrive.Client.Hubs {
	public interface IClientHub {
		Task HackCreateConnection();

		Task ConnectAsync();

		Task DisconnectAsync();

		Task CreateGameAsync( string gameName, string playerName );

		Task DeleteGameAsync( Id<Game> gameId );

		Task LoadGameAsync( Id<Game> gameId );
	}
}
