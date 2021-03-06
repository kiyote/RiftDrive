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
namespace RiftDrive.Server.Repository.S3 {
	public sealed class S3Options {

		public string? CredentialsFile { get; set; }

		public string? CredentialsProfile { get; set; }

		public string? RegionEndpoint { get; set; }

		public string? ServiceUrl { get; set; }

		public string? Role { get; set; }

		public string? Bucket { get; set; }
	}
}
