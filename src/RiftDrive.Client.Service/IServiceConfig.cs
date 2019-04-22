using System;
using System.Collections.Generic;
using System.Text;

namespace RiftDrive.Client.Service {
	public interface IServiceConfig {

		string Host { get; }

		string CognitoClientId { get; }

		string RedirectUrl { get; }

		string TokenUrl { get; }
	}
}
