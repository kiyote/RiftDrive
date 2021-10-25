using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RiftDrive.Client.Actions;

namespace RiftDrive.Client.Pages.Auth {
	public class ValidatePageBase: ComponentBase {

		public const string Url = "/auth/validate";

		public ValidatePageBase() {
			Messages = new List<string>();
		}

		[Inject] protected NavigationManager UriHelper { get; set; }

		[Inject] protected IIdentificationDispatch Dispatch { get; set; }

		[Inject] protected IIdentificationState State { get; set; }

		protected List<string> Messages { get; set; }

		protected int Progress { get; set; }

		protected override async Task OnInitializedAsync() {
			string code = UriHelper.GetParameter( "code" );
			Update( "...retrieving token...", 33 );
			await Dispatch.GetTokens( code );

			Update( "...recording login...", 66 );
			await Dispatch.RecordLogin();

			Update( "...completing login...", 90 );
			await Dispatch.CompleteLogin();

			UriHelper.NavigateTo( IndexPageBase.Url );
		}

		private void Update( string message, int progress ) {
			Messages.Add( message );
			Progress = progress;
			StateHasChanged();
		}
	}
}
