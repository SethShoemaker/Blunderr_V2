using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
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
            UserListResponse r = new()
            {
                CanManageUsers = request.accessorRole == UserRole.Manager,
            };

            if(request.accessorRole == UserRole.Client){
                r.Error = Error.Forbidden;
                return r;
            }

            IQueryable<User> users = _context.Users;

            if(request.role is not null)
                users = users.Where(u => u.Role == request.role);

            IQueryable<UserDto> userDtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Role = u.Role,
                CanBeAssignedTickets = u.Role != UserRole.Client
            });

            r.Page = await userDtos.PaginateAsync<UserDto>(request.pageNumber, request.pageSize, cancellationToken);

            return r;
        }
    }
}