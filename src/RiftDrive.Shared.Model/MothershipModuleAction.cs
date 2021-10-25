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
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RiftDrive.Shared.Model {
	public sealed class MothershipModuleAction : IEquatable<MothershipModuleAction> {

		[JsonConstructor]
		public MothershipModuleAction(
			Id<MothershipModuleAction> id,
			string name,
			string description,
			IEnumerable<MothershipModuleEffect> effects
		) {
			Id = id;
			Name = name;
			Description = description;
			Effects = effects;
		}

		public Id<MothershipModuleAction> Id { get; }

		public string Name { get; }

		public string Description { get; }

		public IEnumerable<MothershipModuleEffect> Effects { get; }

		public bool Equals( MothershipModuleAction? other ) {
			if (other is null) {
				return false;
			}

			if( ReferenceEquals( other, this ) ) {
				return true;
			}

			return Id.Equals( other.Id )
				&& string.Equals( Name, other.Name, StringComparison.Ordinal )
				&& string.Equals( Description, other.Description, StringComparison.Ordinal )
				&& Effects.Similar( other.Effects );
		}

		public override bool Equals( object? obj ) {
			if( !( obj is MothershipModuleAction target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			return HashCode.Combine( Id, Name, Description, Effects );
		}
	}
}
