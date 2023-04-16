using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Users.UserShow
{
    public record UserShowRequest(
        UserRole accessorRole,
        int accessorId,
        int userId
    ) : IRequest<UserShowResponse>;
}