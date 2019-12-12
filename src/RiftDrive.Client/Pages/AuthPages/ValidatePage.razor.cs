/*
 * Copyright 2018-2019 Todd Lang
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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RiftDrive.Client.Action;
using RiftDrive.Client.Service;
using RiftDrive.Client.State;
using RiftDrive.Shared.Message;

namespace RiftDrive.Client.Pages.AuthPages {
	public class ValidatePageBase : ComponentBase, IDisposable {

		public static string Url = "/auth/validate";
		private bool _disposed = false;

		public ValidatePageBase() {
			Messages = new List<string>();
			UriHelper = NullUriHelper.Instance;
			Dispatch = NullDispatch.Instance;
			State = NullAppState.Instance;
			TokenService = NullTokenService.Instance;
		}

		[Inject] protected NavigationManager UriHelper { get; set; }

		[Inject] protected IDispatch Dispatch { get; set; }

		[Inject] protected IAppState State { get; set; }

		[Inject] protected ITokenService TokenService { get; set; }

		protected List<string> Messages { get; set; }

		protected int Progress { get; set; }

		public void Dispose() {
			Dispose( true );
			GC.SuppressFinalize( this );
		}

		protected override async Task OnInitializedAsync() {
			State.OnStateChanged += AppStateHasChanged;
			await State.Initialize();

			string code = UriHelper.GetParameter( "code" );
			Messages.Add( "...retrieving token..." );
			AuthorizationToken? tokens = await TokenService.GetToken( code );
			if( tokens == default ) {
				//TODO: Do something here
				throw new InvalidOperationException();
			}
			await Dispatch.UpdateTokens( tokens.id_token, tokens.refresh_token, DateTime.UtcNow.AddSeconds( tokens.expires_in ) );

			Update( "...recording login...", 50 );
			await Dispatch.RecordLogin();

			Update( "...loading user information...", 75 );
			await Dispatch.LoadUserInformation();

			UriHelper.NavigateTo( IndexPageBase.Url );
		}

		protected virtual void Dispose( bool disposing ) {
			if (_disposed) {
				return;
			}


			if( disposing ) {
				State.OnStateChanged -= AppStateHasChanged;
			}

			_disposed = true;
		}

		private void AppStateHasChanged( object sender, EventArgs e ) {
			StateHasChanged();
		}

		private void Update( string message, int progress ) {
			Messages.Add( message );
			Progress = progress;
			StateHasChanged();
		}
	}
}
