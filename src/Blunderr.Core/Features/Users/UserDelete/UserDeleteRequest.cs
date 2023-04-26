using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Users.UserDelete
{
    public record UserDeleteRequest(
        UserRole deleterRole,
        int deleterId,
        int userId
    ) : IRequest<UserDeleteResponse>;
}