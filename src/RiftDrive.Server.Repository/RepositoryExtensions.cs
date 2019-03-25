using BlazorSpa.Server.Repository.DynamoDb;
using Microsoft.Extensions.DependencyInjection;
using RiftDrive.Server.Repository.Cognito;
using RiftDrive.Server.Repository.S3;

namespace RiftDrive.Server.Repository {
	public static class RepositoryExtensions {
		public static IServiceCollection RegisterRepositories( this IServiceCollection services ) {
			services.AddSingleton<IImageRepository, ImageRepository>();
			services.AddSingleton<IUserRepository, UserRepository>();
			services.AddSingleton<IAuthenticationRepository, AuthenticationRepository>();

			return services;
		}
	}
}
