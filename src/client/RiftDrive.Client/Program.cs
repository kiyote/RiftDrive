using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RiftDrive.Client.Actions;
using RiftDrive.Client.Hubs;
using RiftDrive.Client.Providers;
using RiftDrive.Client.Services.Identification;
using RiftDrive.Common.Serialization;

namespace RiftDrive.Client {
	public class Program {
		public static async Task Main( string[] args ) {
			var builder = WebAssemblyHostBuilder.CreateDefault( args );

			ConfigureServices( builder.Services );

			builder.RootComponents.Add<App>( "app" );
			builder.Services.AddSingleton( new HttpClient {
				BaseAddress = new Uri( builder.HostEnvironment.BaseAddress )
			} );
			WebAssemblyHost host = builder.Build();

			// Access services here

			await host.RunAsync();
		}

		private static void ConfigureServices( IServiceCollection services ) {
			services.AddSingleton<IJSRuntimeProvider, JSRuntimeProvider>();
			services.AddSingleton<IStateProvider, StateProvider>();
			services.AddSingleton<Config>();
			services.AddJsonSerializer();
			services.AddIdentificationService( ( opts ) => {
				opts.CognitoClientId = Config.CognitoClientId;
				opts.RedirectUrl = Config.RedirectUrl;
				opts.TokenUrl = Config.TokenUrl;
				opts.ApiHost = Config.ApiHost;
			} );
			services.AddSingleton<IClientHub, ClientHub>();

			services.AddActions();
		}
	}
}
