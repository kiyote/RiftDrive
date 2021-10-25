using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Amazon.DynamoDBv2.DataModel;

namespace RiftDrive.Server.Services.PitBoss.Model {

	[SuppressMessage( "Performance", "CA1812", Justification = "Record binding" )]
	internal sealed class MissionRecord {

		public const string ItemType = "Mission-";

		public MissionRecord() {
			MissionId = "";
			GameId = "";
			Status = "";
			EncounterCardId = "";
			RaceId = "";
			EncounterOutcomeCardId = "";
			CreatedOn = DateTime.MinValue;
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
				return GetKey( MissionId );
			}
			set {
				MissionId = GetIdFromKey( value );
			}
		}

		[DynamoDBIgnore]
		public string GameId { get; set; }

		[DynamoDBIgnore]
		public string MissionId { get; set; }

		[DynamoDBProperty( "Status" )]
		public string Status { get; set; }

		[DynamoDBProperty( "CreatedOn" )]
		public DateTime CreatedOn { get; set; }

		[DynamoDBProperty( "EncounterCardId" )]
		public string EncounterCardId { get; set; }

		[DynamoDBProperty( "RaceId" )]
		public string RaceId { get; set; }

		[DynamoDBProperty( "EncounterOutcomeCardId " )]
		public string EncounterOutcomeCardId { get; set; }

		public static string GetKey( string missionId ) {
			return $"{ItemType}{missionId}";
		}

		public static string GetIdFromKey( string key ) {
			return key.Substring( ItemType.Length );
		}
	}
}
