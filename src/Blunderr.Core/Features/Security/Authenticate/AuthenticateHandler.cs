using Blunderr.Core.Data;
using Blunderr.Core.Services.TokenService;
using MediatR;

namespace Blunderr.Core.Features.Security.Authenticate
{
    public class AuthenticateHandler : IRequestHandler<AuthenticateRequest, AuthenticateResponse>
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthenticateHandler(AppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<AuthenticateResponse> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
        {
            return new AuthenticateResponse()
            {
                User = await _tokenService.ValidateTokenAsync(request.token)
            };
        }
    }
}