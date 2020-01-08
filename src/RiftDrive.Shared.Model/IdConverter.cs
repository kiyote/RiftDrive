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
using Newtonsoft.Json;

namespace RiftDrive.Shared.Model {
	public class IdConverter : JsonConverter {
		public override bool CanConvert( Type objectType ) {
			return true;
		}

		public override void WriteJson( JsonWriter writer, object? value, JsonSerializer serializer ) {
			writer.WriteValue( value?.ToString() );
		}

		public override bool CanRead {
			get { return true; }
		}

		public override object ReadJson( JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer ) {
			string? value = reader.Value as string;

			if( string.IsNullOrWhiteSpace( value ) ) {
				return Activator.CreateInstance( objectType, new object[] { Guid.Empty.ToString( "N" ) } );
			}

#pragma warning disable CS8601 // Possible null reference assignment.
			return Activator.CreateInstance( objectType, new object[] { value } );
#pragma warning restore CS8601 // Possible null reference assignment.
		}
	}
}
