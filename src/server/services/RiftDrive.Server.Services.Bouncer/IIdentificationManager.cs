using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RiftDrive.Common.Model;

namespace RiftDrive.Server.Services.Bouncer {
	internal interface IIdentificationManager {
		Task<Identification> GetIdentificationAsync( Id<Identification> id );
	}
}
