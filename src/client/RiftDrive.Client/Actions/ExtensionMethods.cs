using System;
using Microsoft.Extensions.DependencyInjection;
using RiftDrive.Client.Providers;

namespace RiftDrive.Client.Actions {
	public static class ExtensionMethods {

		public static IServiceCollection AddActions( this IServiceCollection services ) {
			services.AddSingleton<IStateMonitor, StateMonitor>();
			services.AddSingleton<IIdentificationStateProvider, IdentificationStateProvider>();
			services.AddSingleton<IIdentificationDispatch, IdentificationDispatch>();
			services.AddSingleton<IGameManagementDispatch, GameManagementDispatch>();

			services.AddSingleton<IdentificationState>();
			services.AddSingleton<IIdentificationState>( ( services ) => services.GetService<IdentificationState>() );
			services.AddSingleton<IIdentificationStateMutator>( ( services ) => services.GetService<IdentificationState>() );

			services.AddSingleton<GameManagementState>();
			services.AddSingleton<IGameManagementState>( ( services ) => services.GetService<GameManagementState>() );
			services.AddSingleton<IGameManagementStateMutator>( ( services ) => services.GetService<GameManagementState>() );

			services.AddSingleton<IDispatch, Dispatch>();

			return services;
		}
	}
}
