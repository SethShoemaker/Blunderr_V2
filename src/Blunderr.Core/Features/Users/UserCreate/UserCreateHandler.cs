using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Data.Security.PasswordService;
using Blunderr.Core.Security;
using MediatR;

namespace Blunderr.Core.Features.Users.UserCreate
{
    public class UserCreateHandler : IRequestHandler<UserCreateRequest, UserCreateResponse>
    {
        private readonly AppDbContext _context;
        private readonly IPasswordService _passwordService;

        public UserCreateHandler(AppDbContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task<UserCreateResponse> Handle(UserCreateRequest request, CancellationToken cancellationToken)
        {
            UserCreateResponse r = new();

            if(!UserAccessService.CanCreateUsers(request.CreatorRole)){
                r.Error = Error.Forbidden;
                return r;
            }

            (string hash, string salt) = _passwordService.GenerateHashAndSalt(request.Password);

            User user = new()
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = request.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            r.UserId = user.Id;
            return r;
        }
    }
}