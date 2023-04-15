using Blunderr.Core.Data.Entities.Projects;
using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Tickets.TicketCreate.Query
{
    public class GetCreateDataHandler : IRequestHandler<GetCreateDataRequest, GetCreateDataResponse>
    {
        private readonly AppDbContext _context;

        public GetCreateDataHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GetCreateDataResponse> Handle(GetCreateDataRequest request, CancellationToken cancellationToken)
        {
            return new GetCreateDataResponse()
            {
                Projects = await GetProjectsAsync(request, cancellationToken)
            };
        }

        private async Task<List<ProjectDto>> GetProjectsAsync(GetCreateDataRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Project> projects = _context.Projects;

            if(request.creatorRole is UserRole.Client)
                projects = projects.Where(p => p.ClientId == request.creatorId);

            IQueryable<ProjectDto> projectDtos = projects.Select(p => new ProjectDto()
            {
                Id = p.Id,
                Name = p.Name
            });

            return await projectDtos.ToListAsync(cancellationToken);
        }
    }
}