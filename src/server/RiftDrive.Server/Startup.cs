using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using RiftDrive.Server.Hubs;
using RiftDrive.Server.Middleware;
using RiftDrive.Server.Providers.ContextInformation;
using RiftDrive.Server.Services;
using RiftDrive.Server.Services.Bouncer;
using RiftDrive.Server.Services.PitBoss;

namespace RiftDrive.Server {
	public class Startup {
		public Startup( IConfiguration configuration ) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices( IServiceCollection services ) {

			services
				.AddConnections()
				.AddSignalR( o => o.KeepAliveInterval = TimeSpan.FromSeconds( 5 ) );

			services.AddAuthorization( options => {
				options.AddPolicy( JwtBearerDefaults.AuthenticationScheme, policy => {
					policy.AddAuthenticationSchemes( JwtBearerDefaults.AuthenticationScheme );
					policy.RequireClaim( ClaimTypes.NameIdentifier );
				} );
				options.DefaultPolicy = options.GetPolicy( JwtBearerDefaults.AuthenticationScheme );
			} );

			services
				.AddAuthentication( JwtBearerDefaults.AuthenticationScheme )
				.AddJwtBearer( JwtBearerDefaults.AuthenticationScheme, SetJwtBearerOptions );

			services.AddHttpContextAccessor();
			services.AddControllers();
			services.AddBouncer( Configuration.GetSection("Bouncer") );
			services.AddPitBoss( Configuration.GetSection( "PitBoss" ) );

			services.AddSingleton<IContextInformationProvider, ContextInformationProvider>();
			services.AddSingleton<ISecurePitBossService, SecurePitBossService>();
		}

		public void Configure(
			IApplicationBuilder app,
			IWebHostEnvironment env
		) {
			if( env.IsDevelopment() ) {
				app.UseDeveloperExceptionPage();
				app.UseWebAssemblyDebugging();
			} else {
				app.UseExceptionHandler( "/Error" );
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();
			app.UseRouting();

			app.UseAuthorization();
			app.UseAuthentication();
			app.UseMiddleware<ContextInformationMiddleware>();

			app.UseEndpoints( endpoints => {
				endpoints.MapControllers();
				endpoints.MapHub<ServerHub>( "/hub" );
				endpoints.MapFallbackToFile( "index.html" );
			} );
		}

		private void SetJwtBearerOptions( JwtBearerOptions options ) {
			TokenValidationOptions tokenValidationOptions = Configuration.GetSection( "TokenValidation" ).Get<TokenValidationOptions>();
			var rsa = new RSACryptoServiceProvider();
			rsa.ImportParameters(
				new RSAParameters() {
					Modulus = Base64UrlEncoder.DecodeBytes( tokenValidationOptions.Modulus ),
					Exponent = Base64UrlEncoder.DecodeBytes( tokenValidationOptions.Expo )
				} );
			var key = new RsaSecurityKey( rsa );

			options.RequireHttpsMetadata = false;
			options.TokenValidationParameters = new TokenValidationParameters {
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = key,
				ValidIssuer = tokenValidationOptions.Issuer,
				ValidateIssuer = true,
				ValidateLifetime = true,
				ValidateAudience = false,
				ClockSkew = TimeSpan.FromMinutes( 0 )
			};

			options.Events = new JwtBearerEvents {
				OnMessageReceived = context => {
					StringValues accessToken = context.Request.Query["access_token"];

					// If there is an access token supplied in the url, then
					// we check to see if we're actually trying to service
					// a SignalR request, and if so, we tuck the token in to
					// the context so the request is properly authenticated.
					if( !string.IsNullOrWhiteSpace( accessToken )
						&& ( context.HttpContext.WebSockets.IsWebSocketRequest
							|| context.HttpContext.Request.Path.StartsWithSegments( "/hub", StringComparison.InvariantCulture )
							|| context.Request.Headers["Accept"] == "text/event-stream" )
					) {
						context.Token = context.Request.Query["access_token"];
					}
					return Task.CompletedTask;
				}
			};
		}
	}
}
