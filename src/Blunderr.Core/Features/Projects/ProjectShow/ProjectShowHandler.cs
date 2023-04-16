using Blunderr.Core.Data.Entities.Projects;
using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Projects.ProjectShow
{
    public class ProjectShowHandler : IRequestHandler<ProjectShowRequest, ProjectShowResponse>
    {
        private readonly AppDbContext _context;

        public ProjectShowHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectShowResponse> Handle(ProjectShowRequest request, CancellationToken cancellationToken)
        {
            ProjectShowResponse r = new();

            if(!ProjectAccessService.CanViewProjects(request.accessorRole)){
                r.Error = Error.Forbidden;
                return r;
            }

            ProjectDto? project = await GetProjectAsync(request, cancellationToken);
            if(project is null)
                r.Error = Error.NotFound;

            return r;
        }

        private async Task<ProjectDto?> GetProjectAsync(ProjectShowRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Project> project = _context.Projects
                .Where(p => p.Id == request.projectId)
                .ApplySecurityFilter(request.accessorRole, request.accessorId);

            IQueryable<ProjectDto> projectDto = project.Select(p => new ProjectDto()
            {
                Name = p.Name,
                Client = new ClientDto
                    {
                        Id = p.Client.Id,
                        Name = p.Client.Name
                    },
                Created = p.Created,
                NumPendingTickets = p.Tickets.Where(t => t.Status == TicketStatus.Pending).Count(),
                NumOpenTickets = p.Tickets.Where(t => t.Status == TicketStatus.Open).Count(),
                NumResolvedTickets = p.Tickets.Where(t => t.Status == TicketStatus.Resolved).Count()
            });

            return await projectDto.FirstOrDefaultAsync(cancellationToken);
        }
    }
}