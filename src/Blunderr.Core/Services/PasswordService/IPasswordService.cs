namespace Blunderr.Core.Services.PasswordService
{
    public interface IPasswordService
    {
        public (string, string) GenerateHashAndSalt(string password);

        public bool Verify(string proposed, string actualHash, string actualSalt);
    }
}