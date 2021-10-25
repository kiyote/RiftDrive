using System;
using Microsoft.Extensions.DependencyInjection;

namespace RiftDrive.Client.Services.Identification {
	public static class ExtensionMethods {

		public static IServiceCollection AddIdentificationService( this IServiceCollection services, Action<IdentificationOptions> configureOptions ) {
			services.Configure( configureOptions );
			services.AddSingleton<IIdentificationService, IdentificationService>();
			return services;
		}
	}
}
