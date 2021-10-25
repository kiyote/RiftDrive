using System;
using System.Globalization;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Microsoft.Extensions.Options;

namespace RiftDrive.Server.Services.AWS {
	public sealed class DynamoDbContext<T> {

		public DynamoDbContext(
			IOptions<DynamoDbOptions<T>> options
		) {
			Client = CreateClient( options.Value );
			Context = new DynamoDBContext( Client );
		}

		public IDynamoDBContext Context { get; }

		public IAmazonDynamoDB Client { get; }

		private static IAmazonDynamoDB CreateClient( DynamoDbOptions<T> options ) {
			if( options is null ) {
				throw new InvalidOperationException();
			}

			var chain = new CredentialProfileStoreChain( options.CredentialsFile );
			if( !chain.TryGetAWSCredentials( options.CredentialsProfile, out AWSCredentials credentials ) ) {
				throw new InvalidOperationException();
			}
			var roleCredentials = new AssumeRoleAWSCredentials(
				credentials,
				options.Role,
				Guid.NewGuid().ToString( "N", CultureInfo.InvariantCulture ) );

			AmazonDynamoDBConfig config = new AmazonDynamoDBConfig();
			config.RegionEndpoint = RegionEndpoint.GetBySystemName( options.RegionEndpoint );
			config.ServiceURL = options.ServiceUrl;
			config.LogMetrics = true;
			config.DisableLogging = false;
			return new AmazonDynamoDBClient( roleCredentials, config );
		}
	}
}
