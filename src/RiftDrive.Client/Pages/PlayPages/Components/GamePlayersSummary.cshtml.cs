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
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using RiftDrive.Client.Model;
using RiftDrive.Shared;

namespace RiftDrive.Client.Pages.PlayPages.Components {
	public class GamePlayersSummaryComponent : ComponentBase {

		public GamePlayersSummaryComponent() {
			EditPlayerName = "";
		}

#nullable disable
		protected Modal ModalRef { get; set; }
#nullable enable

		[Parameter] protected IEnumerable<Player> Players { get; set; }

		[Parameter] protected Game Game { get; set; }

		protected string EditPlayerName { get; set; }

		public Task EditPlayer( Player player ) {
			ModalRef.Show();
			return Task.CompletedTask;
		}

		public Task CancelEdit() {
			ModalRef.Hide();
			return Task.CompletedTask;
		}

		public Task ApplyChanges() {
			ModalRef.Hide();
			return Task.CompletedTask;
		}
	}
}