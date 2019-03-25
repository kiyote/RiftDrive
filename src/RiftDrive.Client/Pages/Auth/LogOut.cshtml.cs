using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using RiftDrive.Client.Services;

namespace RiftDrive.Client.Pages.Auth
{
    public class LogOutComponent : ComponentBase
    {
		public const string Url = "/auth/logout";

		[Inject] private IUriHelper _uriHelper { get; set; }

		[Inject] private IAccessTokenProvider _tokens { get; set; }

		protected override async Task OnInitAsync() {
			await _tokens.SetTokens( default, default, DateTime.MinValue );
			_uriHelper.NavigateTo( IndexComponent.Url );
		}
	}

}
