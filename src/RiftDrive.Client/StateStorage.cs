using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using RiftDrive.Client.State;

namespace RiftDrive.Client {
	internal sealed class StateStorage : IStateStorage {

		private readonly IJSRuntime _js;

		public StateStorage( IJSRuntime jsRuntime ) {
			_js = jsRuntime;
		}

		public async Task<DateTime> GetAsDateTime( string name ) {
			string value = await _js.InvokeAsync<string>( "appState.getItem", name );

			if( string.IsNullOrWhiteSpace( value ) ) {
				return DateTime.MinValue.ToUniversalTime();
			}

			return DateTime.Parse( value ).ToUniversalTime();
		}

		public async Task<string> GetAsString( string name ) {
			return await _js.InvokeAsync<string>( "appState.getItem", name );
		}

		public async Task<int> GetAsInt( string name ) {
			string value = await _js.InvokeAsync<string>( "appState.getItem", name );
			return int.Parse( value );
		}

		public async Task Set( string name, string value ) {
			await _js.InvokeAsync<string>( "appState.setItem", name, value );
		}

		public async Task Set( string name, int value ) {
			await _js.InvokeAsync<string>( "appState.setItem", name, value );
		}

		public async Task Set( string name, DateTime value ) {
			await _js.InvokeAsync<string>( "appState.setItem", name, value.ToString( "o" ) );
		}
	}
}
