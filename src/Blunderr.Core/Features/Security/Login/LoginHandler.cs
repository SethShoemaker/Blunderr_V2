using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Services.PasswordService;
using Blunderr.Core.Services.TokenService;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Security.Login
{
    public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly DbSet<User> _users;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public LoginHandler(AppDbContext context, IPasswordService passwordService, ITokenService tokenService)
        {
            _users = context.Users;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            LoginResponse response = new();

            User? user = await _users
                .Where(u => u.Email == request.email)
                .FirstOrDefaultAsync();

            if(user is null)
                response.Errors.Add(Error.UserNotFound);
            else if(!_passwordService.Verify(request.password, user.PasswordHash, user.PasswordSalt))
                response.Errors.Add(Error.IncorrectPassword);
            else
                response.token = await _tokenService.GenerateTokenAsync(user);

            return response;
        }
    }
}