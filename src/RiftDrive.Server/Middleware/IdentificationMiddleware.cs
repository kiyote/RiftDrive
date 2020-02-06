/*
 * Copyright 2018-2020 Todd Lang
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
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using RiftDrive.Server.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace RiftDrive.Server.Middleware {
	public sealed class IdentificationMiddleware {

		private readonly RequestDelegate _next;
		private readonly IUserRepository _userRepository;

		public IdentificationMiddleware(
			RequestDelegate next,
			IUserRepository userRepository
		) {
			_next = next;
			_userRepository = userRepository;
		}

		public async Task InvokeAsync( HttpContext httpContext ) {
			ClaimsIdentity? principal = httpContext.User?.Identities?.FirstOrDefault();

			if( ( principal != default )
				&& (principal.Claims != default)
				&& principal.Claims.Any()
			) {
				string userIdValue = principal.Claims.FirstOrDefault( c => c.Type == ClaimTypes.NameIdentifier ).Value;
				string username = principal.Claims.FirstOrDefault( c => c.Type == "cognito:username" ).Value;

				httpContext.Items["Username"] = username;

				Model.User? user = await _userRepository.GetByUsername( username );
				if( user != default ) {
					httpContext.Items["UserId"] = user.Id.Value;
					httpContext.Items["Name"] = user.Name;
				}
			}

			await _next( httpContext );
		}
	}

	public static class IdentificationMiddlewareExtensions {
		public static IApplicationBuilder UseIdentificationMiddleware( this IApplicationBuilder builder ) {
			return builder.UseMiddleware<IdentificationMiddleware>();
		}
	}
}
