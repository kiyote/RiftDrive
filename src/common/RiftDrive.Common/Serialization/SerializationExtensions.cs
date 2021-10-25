using System;
using Microsoft.Extensions.DependencyInjection;
using Utf8Json;
using Utf8Json.Resolvers;

namespace RiftDrive.Common.Serialization {
	public static class SerializationExtensions {

		public static IServiceCollection AddJsonSerializer( this IServiceCollection services ) {
			JsonSerializer.SetDefaultResolver( StandardResolver.AllowPrivateCamelCase );
			services.AddSingleton<IJsonSerializer, Utf8JsonSerializer>();
			return services;
		}
	}
}
