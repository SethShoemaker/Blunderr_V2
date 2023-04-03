namespace Blunderr.Core.Features.Security.Login
{
    public class LoginResponse
    {
        public List<Error> Errors { get; set; } = null!;

        public string token { get; set; } = null!;

        public bool isSuccessful 
        {
            get => !Errors.Any();
        }
    }

    public enum Error
    {
        UserNotFound,
        IncorrectPassword
    }
}