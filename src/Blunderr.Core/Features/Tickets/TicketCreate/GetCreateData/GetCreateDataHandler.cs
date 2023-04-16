using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Security;
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
            GetCreateDataResponse r = new();

            if(!TicketAccessService.CanCreateTickets(request.creatorRole)){
                r.Error = Error.Forbidden;
                return r;
            }

            r.Projects = await GetProjectsAsync(request, cancellationToken);
            return r;
        }

        private async Task<List<ProjectDto>> GetProjectsAsync(GetCreateDataRequest request, CancellationToken cancellationToken)
        {
            return await _context.Projects
                .ApplySecurityFilter(request.creatorRole, request.creatorId)
                .Select(p => new ProjectDto()
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToListAsync(cancellationToken);
        }
    }
}