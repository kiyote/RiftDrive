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
using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using RiftDrive.Common.Serialization;

namespace RiftDrive.Client.Providers {
	internal sealed class StateProvider : IStateProvider {

		private readonly IJSRuntimeProvider _jsProvider;
		private readonly IJsonSerializer _json;

		public StateProvider(
			IJSRuntimeProvider jsRuntimeProvider,
			IJsonSerializer jsonSerializer
		) {
			_jsProvider = jsRuntimeProvider;
			_json = jsonSerializer;
		}

		public async Task<DateTime> GetAsDateTime( string name ) {
			IJSRuntime js = _jsProvider.Get();
			string value = await js.InvokeAsync<string>( "appState.getItem", name );

			if( string.IsNullOrWhiteSpace( value ) ) {
				return DateTime.MinValue.ToUniversalTime();
			}

			return DateTime.Parse( value ).ToUniversalTime();
		}

		public async Task<string> GetAsString( string name ) {
			IJSRuntime js = _jsProvider.Get();
			return await js.InvokeAsync<string>( "appState.getItem", name );
		}

		public async Task<int> GetAsInt( string name ) {
			IJSRuntime js = _jsProvider.Get();
			string value = await js.InvokeAsync<string>( "appState.getItem", name );
			return int.Parse( value );
		}

		public async Task<T> Get<T>(string name) {
			IJSRuntime js = _jsProvider.Get();
			string value = await js.InvokeAsync<string>( "appState.getItem", name );
			if (string.IsNullOrWhiteSpace(value)) {
				return default!;
			}
			return _json.Deserialize<T>( value );
		}

		public async Task Set( string name, string value ) {
			IJSRuntime js = _jsProvider.Get();
			await js.InvokeAsync<string>( "appState.setItem", name, value );
		}

		public async Task Set( string name, int value ) {
			IJSRuntime js = _jsProvider.Get();
			await js.InvokeAsync<string>( "appState.setItem", name, value );
		}

		public async Task Set( string name, DateTime value ) {
			IJSRuntime js = _jsProvider.Get();
			await js.InvokeAsync<string>( "appState.setItem", name, value.ToString( "o" ) );
		}

		public async Task Set<T>( string name, T value ) {
			string json = _json.Serialize( value );
			IJSRuntime js = _jsProvider.Get();
			await js.InvokeAsync<string>( "appState.setItem", name, json );
		}
	}
}
