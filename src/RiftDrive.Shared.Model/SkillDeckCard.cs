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

namespace RiftDrive.Shared.Model {
	public sealed class SkillDeckCard: IEquatable<SkillDeckCard> {

		public SkillDeckCard(
			Id<SkillCard> skillCardId,
			Id<SkillDeckCard> instanceId
		) {
			SkillCardId = skillCardId;
			InstanceId = instanceId;
		}

		public Id<SkillCard> SkillCardId { get; }

		public Id<SkillDeckCard> InstanceId { get; }

		public bool Equals( SkillDeckCard other ) {
			if (ReferenceEquals( other, this )) {
				return true;
			}

			return SkillCardId.Equals( other.SkillCardId )
				&& InstanceId.Equals( other.InstanceId );
		}

		public override bool Equals( object obj ) {
			if( !( obj is SkillDeckCard target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			return HashCode.Combine( SkillCardId, InstanceId );
		}
	}
}
