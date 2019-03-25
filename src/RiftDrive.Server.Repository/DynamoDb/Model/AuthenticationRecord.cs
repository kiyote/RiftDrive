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
	internal sealed class AuthenticationRecord {

		private const string AuthenticationItemType = "Authentication-";
		public static readonly string StatusActive = "Active";

		[DynamoDBHashKey( "PK" )]
		private string PK {
			get {
				return GetKey( Username );
			}
			set {
				Username = value.Substring( AuthenticationItemType.Length );
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
		public string Username { get; set; }

		[DynamoDBProperty("UserId")]
		public string UserId { get; set; }

		[DynamoDBProperty("DateCreated")]
		public DateTime DateCreated { get; set; }

		[DynamoDBProperty("Status")]
		public string Status { get; set; }

		public static string GetKey(string username) {
			return $"{AuthenticationItemType}{username}";
		}
	}
}
