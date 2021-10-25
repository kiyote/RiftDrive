using System;

namespace RiftDrive.Server.Services.AWS {
	public sealed class DynamoDbOptions<T>: IEquatable<DynamoDbOptions<T>> {

		public string CredentialsProfile { get; set; }

		public string RegionEndpoint { get; set; }

		public string ServiceUrl { get; set; }

		public string CredentialsFile { get; set; }

		public string Role { get; set; }

		public bool Equals( DynamoDbOptions<T> other ) {
			if( other is null ) {
				return false;
			}

			if (ReferenceEquals(other, this)) {
				return true;
			}

			return string.Equals( CredentialsProfile, other.CredentialsProfile, StringComparison.Ordinal )
				&& string.Equals( CredentialsFile, other.CredentialsFile, StringComparison.Ordinal )
				&& string.Equals( RegionEndpoint, other.RegionEndpoint, StringComparison.Ordinal )
				&& string.Equals( ServiceUrl, other.ServiceUrl, StringComparison.Ordinal )
				&& string.Equals( Role, other.Role, StringComparison.Ordinal );
		}

		public override bool Equals( object obj ) {
			if (!(obj is DynamoDbOptions<T> target)) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			return HashCode.Combine( CredentialsProfile, RegionEndpoint, ServiceUrl, CredentialsFile, Role );
		}
	}
}
