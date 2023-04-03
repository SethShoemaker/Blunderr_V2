using Blunderr.Core.Data.Entities.Users;

namespace Blunderr.Core.Features.Security.Authenticate
{
    public class AuthenticateResponse
    {
        public User? User { get; set; }

        public bool isSuccessful
        {
            get => User != null;
        }
    }
}