﻿/*
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
	internal sealed class MothershipAttachedModuleRecord {

		public const string ItemType = "MothershipModule-";

		public MothershipAttachedModuleRecord() {
			MothershipId = "";
			MothershipModuleId = "";
		}


		[DynamoDBHashKey( "PK" )]
		private string PK {
			get {
				return MothershipRecord.GetKey( MothershipId );
			}
			set {
				MothershipId = MothershipRecord.GetIdFromKey( value );
			}
		}

		[DynamoDBRangeKey( "SK" )]
		private string SK {
			get {
				return GetKey( MothershipModuleId );
			}
			set {
				MothershipModuleId = GetIdFromKey( value );
			}
		}

		[DynamoDBIgnore]
		public string MothershipId { get; set; }

		[DynamoDBIgnore]
		public string MothershipModuleId { get; set; }

		[DynamoDBProperty("RemainingPower")]
		public int RemainingPower { get; set; }

		[DynamoDBProperty("CreatedOn")]
		public DateTime CreatedOn { get; set; }

		private static string GetKey( string mothershipModuleId ) {
			return $"{ItemType}{mothershipModuleId}";
		}

		private static string GetIdFromKey( string key ) {
			return key.Substring( ItemType.Length );
		}
	}
}
