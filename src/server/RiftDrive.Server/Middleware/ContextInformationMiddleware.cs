using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RiftDrive.Common.Model;
using RiftDrive.Server.Services.Bouncer;

namespace RiftDrive.Server.Middleware {
	public sealed class ContextInformationMiddleware {

		private readonly RequestDelegate _next;
		private readonly IBouncerService _bouncer;

		public ContextInformationMiddleware(
			RequestDelegate next,
			IBouncerService bouncerService
		) {
			_next = next;
			_bouncer = bouncerService;
		}

		public async Task InvokeAsync( HttpContext httpContext ) {
			if( httpContext is null ) {
				await _next( httpContext ).ConfigureAwait( false );
			} else {
				ClaimsIdentity principal = httpContext.User?.Identities?.FirstOrDefault();

				if( ( principal != default )
					&& ( principal.Claims != default )
					&& principal.Claims.Any()
				) {
					string sub = principal.Claims.FirstOrDefault( c => c.Type == ClaimTypes.NameIdentifier ).Value;
					var identificationId = new Id<Identification>( sub );
					httpContext.Items["identificationId"] = identificationId;

					User user = await _bouncer.GetUserAsync( identificationId ).ConfigureAwait( false );
					httpContext.Items["user"] = user;
				}

				await _next( httpContext ).ConfigureAwait( false );

			}
		}
	}
}
