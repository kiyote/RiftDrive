using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using RiftDrive.Common.Model;

namespace RiftDrive.Server.Providers.ContextInformation {

	[SuppressMessage( "Performance", "CA1812", Justification = "Dependency Injection" )]
	internal sealed class ContextInformationProvider : IContextInformationProvider {

		private readonly IHttpContextAccessor _httpContextAccessor;

		public ContextInformationProvider(
			IHttpContextAccessor httpContextAccessor
		) {
			_httpContextAccessor = httpContextAccessor;
		}

		IContextInformation IContextInformationProvider.GetCurrent() {
			HttpContext context = _httpContextAccessor.HttpContext;
			var result = new ContextInformation(
				context.Items["identificationId"] as Id<Identification>,
				context.Items["user"] as User
				);

			return result;
		}
	}
}
