namespace Blunderr.Core.Features.Users.UserDelete
{
    public class UserDeleteResponse
    {
        public Error? Error { get; set; }
    }

    public enum Error
    {
        Forbidden,
        NotFound
    }
}