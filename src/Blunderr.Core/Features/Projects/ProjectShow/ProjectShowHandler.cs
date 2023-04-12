using Blunderr.Core.Data.Entities.Projects;
using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
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
            return new ProjectShowResponse()
            {
                Project = await GetProjectAsync(request, cancellationToken),
                CanManageProject = request.accessorRole != UserRole.Developer,
                CanViewUsers = request.accessorRole != UserRole.Client
            };
        }

        private async Task<ProjectDto?> GetProjectAsync(ProjectShowRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Project> project = _context.Projects.Where(p => p.Id == request.projectId);

            if(request.accessorRole == UserRole.Client)
                project = project.Where(p => p.ClientId == request.accessorId);

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