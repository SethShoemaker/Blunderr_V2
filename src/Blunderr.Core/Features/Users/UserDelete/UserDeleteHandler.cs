using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Users.UserDelete
{
    public class UserDeleteHandler : IRequestHandler<UserDeleteRequest, UserDeleteResponse>
    {
        private readonly AppDbContext _context;

        public UserDeleteHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserDeleteResponse> Handle(UserDeleteRequest request, CancellationToken cancellationToken)
        {
            UserDeleteResponse r = new();

            if(!UserAccessService.CanDeleteUsers(request.deleterRole)){
                r.Error = Error.Forbidden;
                return r;
            }

            User? user = await GetUserAsync(request, cancellationToken);
            if(user is null){
                r.Error = Error.NotFound;
                return r;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);

            return r;
        }

        private async Task<User?> GetUserAsync(UserDeleteRequest request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.Id == request.userId)
                .Where(u => u.Role != UserRole.Manager)
                .ApplySecurityFilter(request.deleterRole, request.deleterId)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}