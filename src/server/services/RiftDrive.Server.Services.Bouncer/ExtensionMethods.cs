using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiftDrive.Server.Services.AWS;

namespace RiftDrive.Server.Services.Bouncer {
	public static class ExtensionMethods {
		public static IServiceCollection AddBouncer( this IServiceCollection services, IConfigurationSection configuration ) {

			services.Configure<CognitoOptions<IdentificationManager>>( configuration.GetSection( "IdentificationProvider" ) );
			services.AddSingleton<CognitoContext<IdentificationManager>>();
			services.Configure<IdentificationOptions>( configuration.GetSection( "Identification" ) );
			services.AddSingleton<IIdentificationManager, IdentificationManager>();

			services.Configure<DynamoDbOptions<UserManager>>( configuration.GetSection( "UserProvider" ) );
			services.AddSingleton<DynamoDbContext<UserManager>>();
			services.Configure<UserOptions>( configuration.GetSection( "User" ) );
			services.AddSingleton<IUserManager, UserManager>();

			services.AddSingleton<IBouncerService, BouncerService>();

			return services;
		}
	}
}
