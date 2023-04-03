using MediatR;

namespace Blunderr.Core.Features.Security.Login
{
    public record LoginRequest(string email, string password) : IRequest<LoginResponse>;
}