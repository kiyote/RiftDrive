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
	internal sealed class GameMissionRecord {

		public GameMissionRecord() {
			GameId = "";
			MissionId = "";
		}

		[DynamoDBHashKey( "PK" )]
		internal string PK {
			get {
				return GameRecord.GetKey( GameId );
			}
			set {
				GameId = GameRecord.GetIdFromKey( value );
			}
		}

		[DynamoDBRangeKey( "SK" )]
		internal string SK {
			get {
				return MissionRecord.GetKey( MissionId );
			}
			set {
				MissionId = MissionRecord.GetIdFromKey( value );
			}
		}

		[DynamoDBIgnore]
		public string GameId { get; set; }

		[DynamoDBIgnore]
		public string MissionId { get; set; }
	}
}
