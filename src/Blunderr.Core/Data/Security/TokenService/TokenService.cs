using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Data.Security.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly AppDbContext _context;

        public TokenService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateTokenAsync(User user)
        {
            string token = DateTime.Now.Millisecond.ToString();

            _context.UserTokens.Add(new UserToken()
            {
                Value = token,
                User = user
            });

            await _context.SaveChangesAsync();

            return token;
        }

        public async Task<User?> ValidateTokenAsync(string token)
        {
            UserToken? userToken = await _context.UserTokens
                .Where(ut => ut.Value == token)
                .Include(ut => ut.User)
                .FirstOrDefaultAsync();

            return userToken?.User;
        }
    }
}