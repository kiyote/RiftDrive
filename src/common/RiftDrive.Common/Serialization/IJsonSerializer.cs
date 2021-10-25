using System;
using System.Threading.Tasks;

namespace RiftDrive.Common.Serialization {
	public interface IJsonSerializer {

		T Deserialize<T>( string value );

		string Serialize( object value );

		Task<T> DeserializeAsync<T>( string value );

		Task<string> SerializeAsync( object value );
	}
}
