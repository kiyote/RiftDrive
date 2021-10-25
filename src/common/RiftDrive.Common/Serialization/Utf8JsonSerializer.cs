using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Utf8Json;

namespace RiftDrive.Common.Serialization {
	[SuppressMessage("Performance", "CA1812", Justification = "Used in DI.")]
	internal sealed class Utf8JsonSerializer : IJsonSerializer {
		T IJsonSerializer.Deserialize<T>( string value ) {
			if( string.IsNullOrWhiteSpace( value ) ) {
				return default;
			}

			return JsonSerializer.Deserialize<T>( value );
		}

		string IJsonSerializer.Serialize( object value ) {
			if( value is null ) {
				return"{}";
			}
			return JsonSerializer.ToJsonString( value );
		}

		Task<T> IJsonSerializer.DeserializeAsync<T>( string value ) {
			if( string.IsNullOrWhiteSpace( value ) ) {
				return Task.FromResult( default(T) );
			}

			return Task.FromResult( JsonSerializer.Deserialize<T>( value ) );
		}

		Task<string> IJsonSerializer.SerializeAsync( object value ) {
			if( value is null ) {
				return Task.FromResult( "{}" );
			}
			return Task.FromResult( JsonSerializer.ToJsonString( value ) );
		}
	}
}
