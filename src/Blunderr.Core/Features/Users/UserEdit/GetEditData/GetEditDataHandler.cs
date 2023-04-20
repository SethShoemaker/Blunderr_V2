using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Features.Users.UserEdit.SaveEditData;
using Blunderr.Core.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Users.UserEdit.GetEditData
{
    public class GetEditDataHandler : IRequestHandler<GetEditDataRequest, GetEditDataResponse>
    {
        private readonly AppDbContext _context;

        public GetEditDataHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GetEditDataResponse> Handle(GetEditDataRequest request, CancellationToken cancellationToken)
        {
            GetEditDataResponse r = new();

            if(!UserAccessService.CanEditUsers(request.editorRole)){
                r.Error = Error.Forbidden;
                return r;
            }

            SaveEditDataRequest? saveRequest = await GetSaveRequestAsync(request, cancellationToken);
            if(saveRequest is null){
                r.Error = Error.NotFound;
                return r;
            }

            r.SaveRequest = saveRequest;
            return r;
        }

        private async Task<SaveEditDataRequest?> GetSaveRequestAsync(GetEditDataRequest request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.Id == request.userId)
                .ApplySecurityFilter(request.editorRole, request.editorId)
                .Select(u => new SaveEditDataRequest
                {
                    Name = u.Name,
                    Email = u.Email,
                    Phone = u.Phone
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}