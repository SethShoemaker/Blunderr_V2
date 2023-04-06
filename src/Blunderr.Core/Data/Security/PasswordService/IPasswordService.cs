namespace Blunderr.Core.Data.Security.PasswordService
{
    public interface IPasswordService
    {
        public (string, string) GenerateHashAndSalt(string password);

        public bool Verify(string proposed, string actualHash, string actualSalt);
    }
}