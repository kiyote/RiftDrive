using System;
using Amazon.DynamoDBv2.DataModel;

namespace RiftDrive.Server.Services.PitBoss.Model {
	internal sealed class GameStateRecord {

		public const string ItemType = "GameState-";

		public GameStateRecord() {
			GameId = "";
			State = "";
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
				return GetKey( State );
			}
			set {
				State = value.Substring( ItemType.Length );
			}
		}

		[DynamoDBIgnore]
		public string GameId { get; set; }

		[DynamoDBIgnore]
		public string State { get; set; }

		[DynamoDBProperty( "CreatedOn" )]
		public DateTime CreatedOn { get; set; }

		public static string GetKey( string state ) {
			return $"{ItemType}{state}";
		}

		public static string GetIdFromKey( string key ) {
			return key.Substring( ItemType.Length );
		}
	}

}
