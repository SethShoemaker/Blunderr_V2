using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Users.UserList
{
    public record UserListRequest(
        UserRole accessorRole,
        int pageNumber,
        int pageSize,
        UserRole? role
    ) : IRequest<UserListResponse>;
}