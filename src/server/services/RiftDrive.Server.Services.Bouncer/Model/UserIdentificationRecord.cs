using System;
using Amazon.DynamoDBv2.DataModel;

namespace RiftDrive.Server.Services.Bouncer.Model {
	internal sealed class UserIdentificationRecord {

		private const string ItemType = "Identification-";

		public UserIdentificationRecord() {
			UserId = "";
			IdentificationId = "";
		}

		[DynamoDBHashKey( "PK" )]
		[DynamoDBGlobalSecondaryIndexRangeKey( "GSI" )]
		internal string PK {
			get {
				return UserRecord.GetKey( UserId );
			}
			set {
				UserId = UserRecord.GetIdFromKey( value );
			}
		}

		[DynamoDBRangeKey( "SK" )]
		[DynamoDBGlobalSecondaryIndexHashKey( "GSI" )]
		internal string SK {
			get {
				return GetKey( IdentificationId );
			}
			set {
				IdentificationId = GetIdFromKey( value );
			}
		}

		[DynamoDBIgnore]
		public string UserId { get; set; }

		[DynamoDBIgnore]
		public string IdentificationId { get; set; }

		[DynamoDBProperty( "CreatedOn" )]
		public DateTime CreatedOn { get; set; }

		public static string GetKey( string identificationId ) {
			return $"{ItemType}{identificationId}";
		}

		public static string GetIdFromKey( string key ) {
			return key.Substring( ItemType.Length );
		}
	}

}
