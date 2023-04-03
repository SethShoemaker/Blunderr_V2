using System.Security.Cryptography;
using System.Text;

namespace Blunderr.Core.Services.PasswordService
{
    public class PasswordService : IPasswordService
    {
        private const int NUM_ITERATIONS = 20000;

        public (string, string) GenerateHashAndSalt(string password)
        {
            string saltString = new System.Random().Next().ToString();
            byte[] saltBytes = Encoding.Default.GetBytes(saltString);

            byte[] passwordBytes = Encoding.Default.GetBytes(password);

            using(Rfc2898DeriveBytes rfc2898 = new(passwordBytes, saltBytes, NUM_ITERATIONS))
            {
                byte[] hashBytes = rfc2898.GetBytes(20);
                string hashString = Encoding.Default.GetString(hashBytes);

                return (hashString, saltString);
            }
        }

        public bool Verify(string proposed, string actualHash, string actualSalt)
        {
            byte[] saltBytes = Encoding.Default.GetBytes(actualSalt);

            byte[] passwordBytes = Encoding.Default.GetBytes(proposed);

            using(Rfc2898DeriveBytes rfc2898 = new(passwordBytes, saltBytes, NUM_ITERATIONS))
            {
                byte[] passwordHashBytes = rfc2898.GetBytes(20);
                string passwordHash = Encoding.Default.GetString(passwordHashBytes);

                return passwordHash == actualHash;
            }
        }
    }
}