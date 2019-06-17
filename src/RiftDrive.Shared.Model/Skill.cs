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
using Newtonsoft.Json.Converters;

namespace RiftDrive.Shared.Model {
	[JsonConverter( typeof( StringEnumConverter ) )]
	public enum Skill {
		Unknown,

		None,

		Command,

		Security,

		Engineering,

		Science
	}

	public static class SkillExtensions {
		public static Role ToRole(this Skill skill) {
			switch (skill) {
				case Skill.Command:
					return Role.Command;
				case Skill.Engineering:
					return Role.Engineer;
				case Skill.Science:
					return Role.Science;
				case Skill.Security:
					return Role.Security;
				default:
					return Role.Unknown;
			}
		}
	}
}
