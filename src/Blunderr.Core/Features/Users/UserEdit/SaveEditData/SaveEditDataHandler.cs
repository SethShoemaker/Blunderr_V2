using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Data.Security.PasswordService;
using Blunderr.Core.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Users.UserEdit.SaveEditData
{
    public class SaveEditDataHandler : IRequestHandler<SaveEditDataRequest, SaveEditDataResponse>
    {
        private readonly AppDbContext _context;
        private readonly IPasswordService _passwordService;

        public SaveEditDataHandler(AppDbContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task<SaveEditDataResponse> Handle(SaveEditDataRequest request, CancellationToken cancellationToken)
        {
            SaveEditDataResponse r = new();

            if(!UserAccessService.CanEditUsers(request.EditorRole)){
                r.Error = SaveError.Forbidden;
                return r;
            }

            User? user = await GetUserAsync(request, cancellationToken);
            if(user is null){
                r.Error = SaveError.NotFound;
                return r;
            }

            (string hash, string salt) = _passwordService.GenerateHashAndSalt(request.Password);

            user.Name = request.Name;
            user.Email = request.Email;
            user.Phone = request.Phone;
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            await _context.SaveChangesAsync(cancellationToken);
            return r;
        }

        private async Task<User?> GetUserAsync(SaveEditDataRequest request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.Id == request.UserId)
                .ApplySecurityFilter(request.EditorRole, request.EditorId)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}