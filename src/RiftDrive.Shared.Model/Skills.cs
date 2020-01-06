using System;
using System.Collections.Generic;
using System.Linq;

namespace RiftDrive.Shared.Model {
	public sealed partial class Skill {

		public static Skill GetById( Id<Skill> id ) {
			return All.First( s => s.Id == id );
		}

		public static Skill Command = new Skill(
			new Id<Skill>( "7d71e5925307457798a5c97e5c3565d8" ),
			"Command",
			new List<Role>() {
				Role.Command
			},
			new Id<SkillDeck>( "51786b69bbde4fd482e26dfab968f482" ) );

		public static Skill Engineer = new Skill(
			new Id<Skill>( "d130ababbd88494d8442dbf07a841f8f" ),
			"Engineer",
			new List<Role>() {
				Role.Engineer
			},
			new Id<SkillDeck>( "77d18b9d2ff54fc8b7c928c829f31f91" ) );

		public static Skill Science = new Skill(
			new Id<Skill>( "1554b99ad8444627b413f2101baf4923" ),
			"Science",
			new List<Role>() {
				Role.Science
			},
			new Id<SkillDeck>( "a36fad9838384afe9e238c994e482d49" ) );

		public static Skill Security = new Skill(
			new Id<Skill>( "091e4551f8844075af80b6c8ece57f9b" ),
			"Security",
			new List<Role>() {
				Role.Security
			},
			new Id<SkillDeck>( "fa71b8ce820f462d9b7ef6efe188e583" ) );

		public static List<Skill> All = new List<Skill>() {
			Command,
			Engineer,
			Science,
			Security
		};
	}
}
