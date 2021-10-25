using System;
using Amazon.DynamoDBv2.DataModel;

namespace RiftDrive.Server.Services.Bouncer.Model {
	internal sealed class UserRecord {

		internal const string ItemType = "User-";

		public UserRecord() {
			UserId = "";
			Name = "";
		}

		[DynamoDBHashKey( "PK" )]
		[DynamoDBGlobalSecondaryIndexRangeKey( "GSI" )]
		internal string PK {
			get {
				return GetKey( UserId );
			}
			set {
				UserId = GetIdFromKey( value );
			}
		}

		[DynamoDBRangeKey( "SK" )]
		[DynamoDBGlobalSecondaryIndexHashKey( "GSI" )]
		internal string SK {
			get {
				return PK;
			}
			set {
				// Do nothing
			}
		}

		[DynamoDBIgnore]
		public string UserId { get; set; }

		[DynamoDBProperty( "Name" )]
		public string Name { get; set; }

		[DynamoDBProperty( "HasAvatar" )]
		public bool HasAvatar { get; set; }

		[DynamoDBProperty( "LastLogin" )]
		public DateTime LastLogin { get; set; }

		[DynamoDBProperty( "PreviousLogin" )]
		public DateTime? PreviousLogin { get; set; }

		[DynamoDBProperty( "CreatedOn" )]
		public DateTime CreatedOn { get; set; }

		public static string GetKey( string userId ) {
			return $"{ItemType}{userId}";
		}

		public static string GetIdFromKey( string key ) {
			return key.Substring( ItemType.Length );
		}
	}

}
