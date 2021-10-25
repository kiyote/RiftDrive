using System;
using Amazon.DynamoDBv2.DataModel;

namespace RiftDrive.Server.Services.PitBoss.Model {
	internal sealed class PlayerRecord {

		public const string ItemType = "Player-";

		public PlayerRecord() {
			GameId = "";
			UserId = "";
			Name = "";
		}

		[DynamoDBHashKey( "PK" )]
		[DynamoDBGlobalSecondaryIndexRangeKey( "GSI" )]
		private string PK {
			get {
				return GameRecord.GetKey( GameId );
			}
			set {
				GameId = GameRecord.GetIdFromKey( value );
			}
		}

		[DynamoDBRangeKey( "SK" )]
		[DynamoDBGlobalSecondaryIndexHashKey( "GSI" )]
		private string SK {
			get {
				return GetKey( UserId );
			}
			set {
				UserId = value.Substring( ItemType.Length );
			}
		}

		[DynamoDBIgnore]
		public string GameId { get; set; }

		[DynamoDBIgnore]
		public string UserId { get; set; }

		[DynamoDBProperty( "Name" )]
		public string Name { get; set; }

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
