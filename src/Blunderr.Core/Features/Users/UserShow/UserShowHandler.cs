using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Services.PhoneService;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Users.UserShow
{
    public class UserShowHandler : IRequestHandler<UserShowRequest, UserShowResponse>
    {
        private readonly AppDbContext _context;
        private readonly IPhoneService _phone;

        public UserShowHandler(AppDbContext context, IPhoneService phoneService)
        {
            _context = context;
            _phone = phoneService;
        }

        public async Task<UserShowResponse> Handle(UserShowRequest request, CancellationToken cancellationToken)
        {
            UserShowResponse r = new()
            {
                CanManageUser = request.accessorRole == UserRole.Manager
            };

            if(request.accessorRole == UserRole.Client){
                r.Error = Error.Forbidden;
                return r;
            }

            UserDto? user = await GetUserAsync(request, cancellationToken);
            if(user is null)
                r.Error = Error.NotFound;
            else
                r.User = user;
                
            return r;
        }

        private async Task<UserDto?> GetUserAsync(UserShowRequest request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.Id == request.userId)
                .Select(u => new UserDto
                {
                    Name = u.Name,
                    Email = u.Email,
                    Phone = u.Phone == null ? null : _phone.Format((int)u.Phone),
                    Role = u.Role,
                    NumSubmittedTickets = _context.Tickets.Where(t => t.SubmitterId == request.userId).Count(),
                    CanBeAssignedTickets = u.Role != UserRole.Client,
                    NumAssignedTickets = _context.Tickets.Where(t => t.DeveloperId == request.userId).Count(),
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}