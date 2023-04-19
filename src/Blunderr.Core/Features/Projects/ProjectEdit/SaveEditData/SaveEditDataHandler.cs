using Blunderr.Core.Data.Entities.Projects;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Projects.ProjectEdit.SaveEditData
{
    public class SaveEditDataHandler : IRequestHandler<SaveEditDataRequest, SaveEditDataResponse>
    {
        private readonly AppDbContext _context;

        public SaveEditDataHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SaveEditDataResponse> Handle(SaveEditDataRequest request, CancellationToken cancellationToken)
        {
            SaveEditDataResponse r = new();

            if(!ProjectAccessService.CanEditProjects(request.EditorRole)){
                r.Error = SaveError.Forbidden;
                return r;
            }

            Project? project = await GetProjectAsync(request, cancellationToken);
            if(project is null){
                r.Error = SaveError.NotFound;
                return r;
            }

            project.Name = request.Name;
            
            await _context.SaveChangesAsync(cancellationToken);

            return r;
        }

        private async Task<Project?> GetProjectAsync(SaveEditDataRequest request, CancellationToken cancellationToken)
        {
            return await _context.Projects
                .Where(p => p.Id == request.ProjectId)
                .ApplySecurityFilter(request.EditorRole, request.EditorId)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}