using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Security;
using Blunderr.Core.Services.PaginationService;
using MediatR;

namespace Blunderr.Core.Features.Users.UserList
{
    public class UserListHandler : IRequestHandler<UserListRequest, UserListResponse>
    {
        private readonly AppDbContext _context;

        public UserListHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserListResponse> Handle(UserListRequest request, CancellationToken cancellationToken)
        {
            UserListResponse r = new();

            if(!UserAccessService.CanListUsers(request.accessorRole)){
                r.Error = Error.Forbidden;
                return r;
            }

            r.Page = await GetUsersAsync(request, cancellationToken);

            return r;
        }

        private async Task<Page<UserDto>> GetUsersAsync(UserListRequest request, CancellationToken cancellationToken)
        {
            IQueryable<User> users = _context.Users.ApplySecurityFilter(request.accessorRole, request.accessorId);

            if(request.role is not null)
                users = users.Where(u => u.Role == request.role);

            return await users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Role = u.Role,
                    CanBeAssignedTickets = u.Role != UserRole.Client
                })
                .PaginateAsync<UserDto>(request.pageNumber, request.pageSize, cancellationToken);
        }
    }
}