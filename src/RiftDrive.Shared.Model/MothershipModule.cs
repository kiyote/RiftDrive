﻿/*
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
using Newtonsoft.Json;

namespace RiftDrive.Shared.Model {
	public sealed partial class MothershipModule: IEquatable<MothershipModule> {

		[JsonConstructor]
		public MothershipModule(
			Id<MothershipModule> id,
			string name,
			string description,
			IEnumerable<MothershipModuleAction> actions,
			IEnumerable<MothershipModuleEffect> effects
		) {
			Id = id;
			Name = name;
			Description = description;
			Actions = actions;
			Effects = effects;
		}

		public Id<MothershipModule> Id { get; }

		public string Name { get; }

		public string Description { get; }

		public IEnumerable<MothershipModuleAction> Actions { get; }

		public IEnumerable<MothershipModuleEffect> Effects { get; }

		public bool Equals( MothershipModule other ) {
			if (ReferenceEquals(other, this)) {
				return true;
			}

			return Id.Equals( other.Id )
				&& string.Equals( Name, other.Name, StringComparison.Ordinal )
				&& Actions.Similar( other.Actions )
				&& Effects.Similar( other.Effects );
		}

		public override bool Equals( object obj ) {
			if( !( obj is MothershipModule target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			unchecked {
				int result = 17;
				result = ( result * 31 ) + Id.GetHashCode();
				result = ( result * 31 ) + Name.GetHashCode();
				result = ( result * 31 ) + Actions.GetFinalHashCode();
				result = ( result * 31 ) + Effects.GetFinalHashCode();

				return result;
			}
		}
	}
}