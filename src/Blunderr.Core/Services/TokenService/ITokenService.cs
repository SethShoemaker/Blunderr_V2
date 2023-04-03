using Blunderr.Core.Data.Entities.Users;

namespace Blunderr.Core.Services.TokenService
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(User user);

        Task<User?> ValidateTokenAsync(string token);
    }
}