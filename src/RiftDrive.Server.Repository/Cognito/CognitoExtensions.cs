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
using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Microsoft.Extensions.DependencyInjection;

namespace RiftDrive.Server.Repository.Cognito {
	public static class CognitoExtensions {
		public static IServiceCollection AddCognito( this IServiceCollection services, CognitoOptions options ) {
			services.AddSingleton( options );

			var provider = CreateProvider( options );
			services.AddSingleton( provider );

			return services;
		}

		public static IAmazonCognitoIdentityProvider CreateProvider( CognitoOptions options ) {
			var chain = new CredentialProfileStoreChain( options.CredentialsFile );
			AWSCredentials credentials;
			if( !chain.TryGetAWSCredentials( options.CredentialsProfile, out credentials ) ) {
				throw new InvalidOperationException();
			}

			var roleCredentials = new AssumeRoleAWSCredentials(
				credentials,
				options.Role,
				Guid.NewGuid().ToString( "N" ) );

			AmazonCognitoIdentityProviderConfig config = new AmazonCognitoIdentityProviderConfig();
			config.RegionEndpoint = RegionEndpoint.GetBySystemName( options.RegionEndpoint );
			config.ServiceURL = options.ServiceUrl;
			config.LogMetrics = true;
			config.DisableLogging = false;
			var client = new AmazonCognitoIdentityProviderClient( roleCredentials, config );

			return client;
		}
	}
}
