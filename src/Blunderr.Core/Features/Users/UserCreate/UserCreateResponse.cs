namespace Blunderr.Core.Features.Users.UserCreate
{
    public class UserCreateResponse
    {
        public int UserId { get; set; }

        public Error? Error { get; set; }
    }

    public enum Error
    {
        Forbidden
    }
}