using System;
using System.Threading.Tasks;
using RiftDrive.Common.Model;

namespace RiftDrive.Client.Services.Identification
{
    public interface IIdentificationService
    {
		Task<Tokens> GetTokensAsync( string code );

		Task<Tokens> RefreshTokensAsync( string refreshToken );

		Task<User> RecordLogin( string idToken );
    }
}
