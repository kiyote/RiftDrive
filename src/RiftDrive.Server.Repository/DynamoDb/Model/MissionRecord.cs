/*
 * Copyright 2018-2020 Todd Lang
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
using System;
using Amazon.DynamoDBv2.DataModel;

namespace RiftDrive.Server.Repository.DynamoDb.Model {
#if DEBUG
	[DynamoDBTable( "RiftDrive-Development" )]
#else
	[DynamoDBTable( "RiftDrive" )]
#endif
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
		private string PK {
			get {
				return GetKey( MissionId );
			}
			set {
				MissionId = GetIdFromKey( value );
			}
		}

		[DynamoDBRangeKey( "SK" )]
		private string SK {
			get {
				return PK;
			}
			set {
				// Do nothing
			}
		}

		[DynamoDBIgnore]
		public string MissionId { get; set; }

		[DynamoDBProperty("GameId")]
		public string GameId { get; set; }

		[DynamoDBProperty("Status")]
		public string Status { get; set; }

		[DynamoDBProperty( "CreatedOn" )]
		public DateTime CreatedOn { get; set; }

		[DynamoDBProperty( "EncounterCardId" )]
		public string EncounterCardId { get; set; }

		[DynamoDBProperty( "RaceId" )]
		public string RaceId { get; set; }

		[DynamoDBProperty( "EncounterOutcomeCardId ")]
		public string EncounterOutcomeCardId { get; set; }

		public static string GetKey( string missionId ) {
			return $"{ItemType}{missionId}";
		}

		public static string GetIdFromKey( string key ) {
			return key.Substring( ItemType.Length );
		}
	}
}
