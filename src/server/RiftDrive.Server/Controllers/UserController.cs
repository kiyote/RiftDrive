using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RiftDrive.Server.Providers.ContextInformation;
using RiftDrive.Server.Services.Bouncer;
using RiftDrive.Common.Model;
using Microsoft.AspNetCore.Authorization;

namespace RiftDrive.Server.Controllers {

	[Authorize]
	[ApiController]
	[Route( "api/user/" )]
	[Produces( "application/json" )]
	[ResponseCache( Location = ResponseCacheLocation.None, NoStore = true )]
	public class UserController : Controller {

		private readonly IContextInformationProvider _contextInformationProvider;
		private readonly IBouncerService _bouncer;

		public UserController(
			IContextInformationProvider contextInformationProvider,
			IBouncerService bouncerService
		) {
			_contextInformationProvider = contextInformationProvider;
			_bouncer = bouncerService;
		}

		[HttpGet( "identification" )]
		public async Task<ActionResult<Identification>> GetIdentificationAsync() {
			IContextInformation contextInformation = _contextInformationProvider.GetCurrent();
			Identification identification = await _bouncer.GetIdentificationAsync( contextInformation.IdentificationId ).ConfigureAwait( false );
			return Ok( identification );
		}

		[HttpPost( "login" )]
		public async Task<ActionResult<User>> RecordLoginAsync() {
			IContextInformation contextInformation = _contextInformationProvider.GetCurrent();
			User user = await _bouncer.RecordLoginAsync( contextInformation.IdentificationId ).ConfigureAwait( false );
			return Ok( user );
		}
	}
}
