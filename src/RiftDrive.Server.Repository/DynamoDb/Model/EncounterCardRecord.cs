/*
 * Copyright 2018-2019 Todd Lang
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
	internal sealed class EncounterCardRecord {

		public const string ItemType = "EncounterCard-";

		public EncounterCardRecord() {
			EncounterCardId = "";
			Description = "";
			RevealRaceSkill = "";
		}

		[DynamoDBHashKey( "PK" )]
		private string PK {
			get {
				return GetKey( EncounterCardId );
			}
			set {
				EncounterCardId = GetIdFromKey( value );
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
		public string EncounterCardId { get; set; }

		[DynamoDBProperty( "Description" )]
		public string Description { get; set; }

		[DynamoDBProperty( "RevealRaceSkill" )]
		public string RevealRaceSkill { get; set; }

		[DynamoDBProperty( "RevealRaceTarget" )]
		public int RevealRaceTarget { get; set; }

		public static string GetKey( string encounterCardId ) {
			return $"{ItemType}{encounterCardId}";
		}

		public static string GetIdFromKey( string key ) {
			return key.Substring( ItemType.Length );
		}
	}
}
