using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Users.UserShow
{
    public record UserShowRequest(
        UserRole accessorRole,
        int userId
    ) : IRequest<UserShowResponse>;
}