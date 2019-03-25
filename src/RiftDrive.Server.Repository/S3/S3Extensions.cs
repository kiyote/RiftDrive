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
using System.Linq;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Microsoft.Extensions.DependencyInjection;

namespace RiftDrive.Server.Repository.S3 {
	public static class S3Extensions {
		public static IServiceCollection AddS3( this IServiceCollection services, S3Options options ) {
			services.AddSingleton( options );

			var provider = CreateProvider( options );
			services.AddSingleton( provider );
			services.AddSingleton( options );

			return services;
		}

		public static IAmazonS3 CreateProvider( S3Options options ) {
			var chain = new CredentialProfileStoreChain( options.CredentialsFile );
			AWSCredentials credentials;
			if( !chain.TryGetAWSCredentials( options.CredentialsProfile, out credentials ) ) {
				var profiles = chain.ListProfiles();
				throw new InvalidOperationException( profiles.Select( p => p.Name ).Aggregate( ( c, n ) => c + ", " + n ) );
			}
			var roleCredentials = new AssumeRoleAWSCredentials(
				credentials,
				options.Role,
				Guid.NewGuid().ToString( "N" ) );

			AmazonS3Config config = new AmazonS3Config();
			config.RegionEndpoint = RegionEndpoint.GetBySystemName( options.RegionEndpoint );
			config.ServiceURL = options.ServiceUrl;
			config.LogMetrics = true;
			config.DisableLogging = false;
			return new AmazonS3Client( roleCredentials, RegionEndpoint.GetBySystemName( options.RegionEndpoint ) );
		}
	}
}
