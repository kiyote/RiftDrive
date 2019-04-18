using System;
using System.Threading.Tasks;

namespace RiftDrive.Client.State {
	public interface IStateStorage {
		Task<DateTime> GetAsDateTime( string name );
		Task<int> GetAsInt( string name );
		Task<string> GetAsString( string name );
		Task Set( string name, DateTime value );
		Task Set( string name, int value );
		Task Set( string name, string value );
	}
}
