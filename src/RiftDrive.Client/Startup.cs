using Blazorise.Material;
using Blazorise.Icons.Material;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using RiftDrive.Client.Services;
using Blazorise;

namespace RiftDrive.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) {
			services
                .AddBlazorise( options =>
				 {
					 options.ChangeTextOnKeyPress = true;
				 } )
				.AddMaterialProviders()
				.AddMaterialIcons();

			services.AddSingleton<IJsonConverter, JsonConverter>();
			services.AddSingleton<IConfig, Config>();

			services.AddScoped<IAppState, AppState>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IAccessTokenProvider, AccessTokenProvider>();
			services.AddScoped<ISignalService, SignalService>();
			services.AddScoped<IUserApiService, UserApiService>();
			services.AddScoped<IStructureApiService, StructureApiService>();
		}

        public void Configure(IComponentsApplicationBuilder app)
        {
			app
				.UseMaterialProviders()
				.UseMaterialIcons();
			app.AddComponent<App>("app");
        }
    }
}
