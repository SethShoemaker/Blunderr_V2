using Blunderr.Core.Data.Entities.Projects;
using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Projects.ProjectCreate.SaveCreateData
{
    public class SaveCreateDataHandler : IRequestHandler<SaveCreateDataRequest, SaveCreateDataResponse>
    {
        private readonly AppDbContext _context;

        public SaveCreateDataHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SaveCreateDataResponse> Handle(SaveCreateDataRequest request, CancellationToken cancellationToken)
        {
            SaveCreateDataResponse r = new();

            if(!ProjectAccessService.CanCreateProjects(request.CreatorRole)){
                r.Error = SaveError.Forbidden;
                return r;
            }

            User? client = await GetClientAsync(request, cancellationToken);
            if(client is null){
                r.Error = SaveError.ClientNotFound;
                return r;
            }

            Project project = new()
            {
                Name = request.ProjectName,
                Created = DateTime.Now,
                Client = client
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync(cancellationToken);

            r.ProjectId = project.Id;
            return r;
        }

        private async Task<User?> GetClientAsync(SaveCreateDataRequest request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.Id == request.ClientId)
                .Where(u => u.Role == UserRole.Client)
                .ApplySecurityFilter(request.CreatorRole, request.CreatorId)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}