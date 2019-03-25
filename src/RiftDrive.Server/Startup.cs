using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BlazorSpa.Server.Repository.DynamoDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RiftDrive.Server.Hubs;
using RiftDrive.Server.Managers;
using RiftDrive.Server.Middleware;
using RiftDrive.Server.Repository;
using RiftDrive.Server.Repository.Cognito;
using RiftDrive.Server.Repository.S3;
using RiftDrive.Server.Service;

namespace RiftDrive.Server {
	public class Startup {
		public Startup( IConfiguration configuration ) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices( IServiceCollection services ) {
			services
				.AddConnections()
				.AddSignalR( o => o.KeepAliveInterval = TimeSpan.FromSeconds( 5 ) )
				.AddNewtonsoftJsonProtocol();

			services.AddAuthorization( options => {
				options.AddPolicy( JwtBearerDefaults.AuthenticationScheme, policy => {
					policy.AddAuthenticationSchemes( JwtBearerDefaults.AuthenticationScheme );
					policy.RequireClaim( ClaimTypes.NameIdentifier );
				} );
			} );

			services
				.AddAuthentication( JwtBearerDefaults.AuthenticationScheme )
				.AddJwtBearer( JwtBearerDefaults.AuthenticationScheme, SetJwtBearerOptions );

			// TODO: Determine if this is still needed for SignalR
			services.AddCors( options => options.AddPolicy( "CorsPolicy",
				 builder => {
					 builder.AllowAnyMethod()
						 .AllowAnyHeader()
						 .AllowAnyOrigin();
				 } ) );

			services
				.AddMvc()
				.SetCompatibilityVersion( Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0 )
				.AddNewtonsoftJson( options => {
					options.SerializerSettings.ContractResolver = new DefaultContractResolver();
					options.SerializerSettings.DateParseHandling = DateParseHandling.None;
					options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
				} );

			services.AddResponseCompression( opts => {
				opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
					new[] { "application/octet-stream" } );
			} );

			services.AddHttpContextAccessor();

			services.AddDynamoDb( Configuration.GetSection( "DynamoDb" ).Get<DynamoDbOptions>() );
			services.AddCognito( Configuration.GetSection( "Cognito" ).Get<CognitoOptions>() );
			services.AddS3( Configuration.GetSection( "S3" ).Get<S3Options>() );
			services.RegisterRepositories();
			services.RegisterServices();

			services.AddSingleton<IContextInformation, ContextInformation>();
			services.AddSingleton<UserManager>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure( IApplicationBuilder app, IWebHostEnvironment env ) {
			app.UseResponseCompression();
			app.UseAuthentication();
			app.UseIdentificationMiddleware();

			if( env.IsDevelopment() ) {
				app.UseDeveloperExceptionPage();
				app.UseBlazorDebugging();
			}

			app.UseCors( "CorsPolicy" );
			app.UseSignalR( routes => {
				routes.MapHub<SignalHub>( SignalHub.Url );
			} );
			app.UseMvc( routes => {
				routes.MapRoute( name: "default", template: "{controller}/{action}/{id?}" );
			} );

			app.UseBlazor<Client.Startup>();
		}

		private void SetJwtBearerOptions( JwtBearerOptions options ) {
			var tokenValidationOptions = Configuration.GetSection( "TokenValidation" ).Get<TokenValidationOptions>();
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
					var accessToken = context.Request.Query["access_token"];

					// If there is an access token supplied in the url, then
					// we check to see if we're actually trying to service
					// a SignalR request, and if so, we tuck the token in to
					// the context so the request is property authenticated.
					if( !string.IsNullOrWhiteSpace( accessToken )
						&& ( context.HttpContext.WebSockets.IsWebSocketRequest
							|| context.HttpContext.Request.Path.StartsWithSegments( SignalHub.Url )
							|| context.Request.Headers["Accept"] == "text/event-stream" ) ) {
						context.Token = context.Request.Query["access_token"];
					}
					return Task.CompletedTask;
				}
			};
		}
	}
}
