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
using Newtonsoft.Json;

namespace RiftDrive.Client {
	public class JsonConverter : IJsonConverter {

		private readonly JsonSerializerSettings _settings;

		public JsonConverter() {
			_settings = new JsonSerializerSettings() {
				DateParseHandling = DateParseHandling.None,
				DateTimeZoneHandling = DateTimeZoneHandling.Utc,
				DateFormatHandling = DateFormatHandling.IsoDateFormat
			};
		}

		T IJsonConverter.Deserialize<T>( string value ) {
			return JsonConvert.DeserializeObject<T>( value, _settings );
		}

		string IJsonConverter.Serialize( object value ) {
			if( value == default ) {
				return "{}";
			}
			return JsonConvert.SerializeObject( value, _settings );
		}
	}
}
