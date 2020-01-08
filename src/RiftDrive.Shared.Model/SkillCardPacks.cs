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
using System.Collections.Generic;

namespace RiftDrive.Shared.Model {
	public sealed partial class SkillCardPack {

		public readonly static SkillCardPack Command = new SkillCardPack(
			new Id<SkillCardPack>( "51786b69bbde4fd482e26dfab968f482" ),
			new List<Id<SkillCard>>() {			
				SkillCard.Hesitation.Id,
				SkillCard.Hesitation.Id,
				SkillCard.Hesitation.Id,
				SkillCard.Command1.Id,
				SkillCard.Command1.Id,
				SkillCard.Command1.Id,
				SkillCard.Command1.Id,
				SkillCard.Command1.Id,
				SkillCard.Command1.Id,
				SkillCard.Command1.Id
			}
		);

		public readonly static SkillCardPack Science = new SkillCardPack(
			new Id<SkillCardPack>( "a36fad9838384afe9e238c994e482d49" ),
			new List<Id<SkillCard>>() {
				SkillCard.Hesitation.Id,
				SkillCard.Hesitation.Id,
				SkillCard.Hesitation.Id,
				SkillCard.Science1.Id,
				SkillCard.Science1.Id,
				SkillCard.Science1.Id,
				SkillCard.Science1.Id,
				SkillCard.Science1.Id,
				SkillCard.Science1.Id,
				SkillCard.Science1.Id
			}
		);

		public readonly static SkillCardPack Engineering = new SkillCardPack(
			new Id<SkillCardPack>( "77d18b9d2ff54fc8b7c928c829f31f91" ),
			new List<Id<SkillCard>>() {
				SkillCard.Hesitation.Id,
				SkillCard.Hesitation.Id,
				SkillCard.Hesitation.Id,
				SkillCard.Engineering1.Id,
				SkillCard.Engineering1.Id,
				SkillCard.Engineering1.Id,
				SkillCard.Engineering1.Id,
				SkillCard.Engineering1.Id,
				SkillCard.Engineering1.Id,
				SkillCard.Engineering1.Id
			}
		);

		public readonly static SkillCardPack Security = new SkillCardPack(
			new Id<SkillCardPack>( "fa71b8ce820f462d9b7ef6efe188e583" ),
			new List<Id<SkillCard>>() {
				SkillCard.Hesitation.Id,
				SkillCard.Hesitation.Id,
				SkillCard.Hesitation.Id,
				SkillCard.Security1.Id,
				SkillCard.Security1.Id,
				SkillCard.Security1.Id,
				SkillCard.Security1.Id,
				SkillCard.Security1.Id,
				SkillCard.Security1.Id,
				SkillCard.Security1.Id
			}
		);
	}
}
