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
using Microsoft.Extensions.DependencyInjection;
using RiftDrive.Server.Repository.Cognito;
using RiftDrive.Server.Repository.DynamoDb;
using RiftDrive.Server.Repository.S3;

namespace RiftDrive.Server.Repository {
	public static class RepositoryExtensions {
		public static IServiceCollection RegisterRepositories( this IServiceCollection services ) {
			services.AddSingleton<IImageRepository, ImageRepository>();
			services.AddSingleton<IUserRepository, UserRepository>();
			services.AddSingleton<IAuthenticationRepository, AuthenticationRepository>();
			services.AddSingleton<IGameRepository, GameRepository>();
			services.AddSingleton<IPlayerRepository, PlayerRepository>();

			return services;
		}
	}
}
