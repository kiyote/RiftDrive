using System;
using System.Globalization;
using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Microsoft.Extensions.Options;

namespace RiftDrive.Server.Services.AWS {
	public sealed class CognitoContext<T> {

		public CognitoContext(
			IOptions<CognitoOptions<T>> options
		) {
			Provider = CreateCognitoProvider( options.Value );
		}

		public IAmazonCognitoIdentityProvider Provider { get; }

		private static IAmazonCognitoIdentityProvider CreateCognitoProvider( CognitoOptions<T> options ) {
			var chain = new CredentialProfileStoreChain( options.CredentialsFile );
			AWSCredentials credentials;
			if( !chain.TryGetAWSCredentials( options.CredentialsProfile, out credentials ) ) {
				throw new InvalidOperationException();
			}

			var roleCredentials = new AssumeRoleAWSCredentials(
				credentials,
				options.Role,
				Guid.NewGuid().ToString( "N", CultureInfo.InvariantCulture ) );

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
