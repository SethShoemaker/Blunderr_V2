using MediatR;

namespace Blunderr.Core.Features.Security.Authenticate
{
    public record AuthenticateRequest(string token) : IRequest<AuthenticateResponse>;
}