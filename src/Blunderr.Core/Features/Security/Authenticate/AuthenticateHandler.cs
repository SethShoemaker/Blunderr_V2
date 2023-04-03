using Blunderr.Core.Services.TokenService;
using MediatR;

namespace Blunderr.Core.Features.Security.Authenticate
{
    public class AuthenticateHandler : IRequestHandler<AuthenticateRequest, AuthenticateResponse>
    {
        private readonly ITokenService _tokenService;

        public AuthenticateHandler(ITokenService tokenService)
        {
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