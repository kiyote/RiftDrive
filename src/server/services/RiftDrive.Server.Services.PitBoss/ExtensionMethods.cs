using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiftDrive.Server.Services.AWS;

namespace RiftDrive.Server.Services.PitBoss {
	public static class ExtensionMethods {
		public static IServiceCollection AddPitBoss( this IServiceCollection services, IConfigurationSection configuration ) {

			if (configuration is null) {
				throw new ArgumentException( "PitBoss configuration missing", nameof( configuration ) );
			}

			services.Configure<DynamoDbOptions<GameRepository>>( configuration.GetSection( "GameProvider" ) );
			services.AddSingleton<DynamoDbContext<GameRepository>>();
			services.Configure<GameOptions>( configuration.GetSection( "Game" ) );
			services.AddSingleton<IGameRepository, GameRepository>();

			services.Configure<DynamoDbOptions<MissionRepository>>( configuration.GetSection( "MissionProvider" ) );
			services.AddSingleton<DynamoDbContext<MissionRepository>>();
			services.Configure<GameOptions>( configuration.GetSection( "Mission" ) );
			services.AddSingleton<IMissionRepository, MissionRepository>();

			services.AddSingleton<IGameManager, GameManager>();
			services.AddSingleton<IPitBossService, PitBossService>();
			return services;
		}
	}
}
