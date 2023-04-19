using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Features.Projects.ProjectEdit.SaveEditData;
using Blunderr.Core.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Projects.ProjectEdit.GetEditData
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

            if(!ProjectAccessService.CanEditProjects(request.editorRole)){
                r.Error = Error.Forbidden;
                return r;
            }

            SaveEditDataRequest? saveRequest = await GetSaveEditDataRequestAsync(request, cancellationToken);
            if(saveRequest is null){
                r.Error = Error.NotFound;
                return r;
            }

            r.SaveRequest = saveRequest;
            return r;
        }

        private async Task<SaveEditDataRequest?> GetSaveEditDataRequestAsync(GetEditDataRequest request, CancellationToken cancellationToken)
        {
            return await _context.Projects
                .Where(p => p.Id == request.projectId)
                .ApplySecurityFilter(request.editorRole, request.editorId)
                .Select(p => new SaveEditDataRequest{ Name = p.Name })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}