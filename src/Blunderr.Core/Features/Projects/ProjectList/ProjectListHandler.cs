using Blunderr.Core.Data.Entities.Projects;
using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Security;
using Blunderr.Core.Services.PaginationService;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Projects.ProjectList
{
    public class ProjectListHandler : IRequestHandler<ProjectListRequest, ProjectListResponse>
    {
        private readonly AppDbContext _context;

        public ProjectListHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectListResponse> Handle(ProjectListRequest request, CancellationToken cancellationToken)
        {
            ProjectListResponse r = new();

            if(!ProjectAccessService.CanListProjects(request.accessorRole)){
                r.Error = Error.Forbidden;
                return r;
            }

            r.Page = await GetProjectPageAsync(request, cancellationToken);
            r.Clients = await GetClientsAsync(request, cancellationToken);

            return r;
        }

        private async Task<Page<ProjectDto>> GetProjectPageAsync(ProjectListRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Project> projects = _context.Projects
                .ApplySecurityFilter(request.accessorRole, request.accessorId);

            if(request.clientId is not null)
                projects = projects.Where(p => p.ClientId == request.clientId);

            IQueryable<ProjectDto> projectDtos = projects.Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Client = new ClientDto 
                    { 
                        Id = p.Client.Id,
                        Name = p.Client.Name 
                    },
                Created = p.Created
            });

            return await projectDtos.PaginateAsync<ProjectDto>(request.pageNumber, request.pageSize, cancellationToken);
        }

        private async Task<List<ClientDto>> GetClientsAsync(ProjectListRequest request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.Role == UserRole.Client)
                .ApplySecurityFilter(request.accessorRole, request.accessorId)
                .Select(c => new ClientDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync(cancellationToken);
        }
    }
}