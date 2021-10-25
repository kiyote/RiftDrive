using System;
using System.Collections.Generic;
using System.Text;

namespace RiftDrive.Client.Services.Identification {
	public sealed class IdentificationOptions {

		public string CognitoClientId { get; set; }

		public string RedirectUrl { get; set; }

		public string TokenUrl { get; set; }

		public string ApiHost { get; set; }
	}
}
