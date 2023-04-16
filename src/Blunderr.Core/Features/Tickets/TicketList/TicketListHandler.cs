using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Security;
using Blunderr.Core.Services.PaginationService;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Tickets.TicketList
{
    public class TicketListHandler : IRequestHandler<TicketListRequest, TicketListResponse>
    {
        private readonly AppDbContext _context;

        public TicketListHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TicketListResponse> Handle(TicketListRequest request, CancellationToken cancellationToken)
        {
            TicketListResponse r = new();

            if(!TicketAccessService.CanListTickets(request.accessorRole)){
                r.Error = Error.Forbidden;
                return r;
            }

            r.Page = await GetTicketDtosAsync(request, cancellationToken);
            r.Submitters = await GetSubmitterDtosAsync(request, cancellationToken);
            r.Projects = await GetProjectDtosAsync(request, cancellationToken);
            r.Developers = await GetDeveloperDtosAsync(request, cancellationToken);

            return r;
        }

        private async Task<Page<TicketDto>> GetTicketDtosAsync(TicketListRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Ticket> tickets = _context.Tickets.ApplySecurityFilter(request.accessorRole, request.accessorId);

            if(request.submitterId is not null)
                tickets = tickets.Where(t => t.SubmitterId == request.submitterId);

            if(request.projectId is not null)
                tickets = tickets.Where(t => t.ProjectId == request.projectId);

            if(request.developerId is not null)
                tickets = tickets.Where(t => t.DeveloperId == request.developerId);

            if(request.ticketStatus is not null)
                tickets = tickets.Where(t => t.Status == request.ticketStatus);

            return await tickets
            .Select(t => new TicketDto
            {
                Id = t.Id,
                Submitter = new UserDto
                    {
                        Id = t.Submitter.Id,
                        Name = t.Submitter.Name
                    },
                Project = new ProjectDto
                    {
                        Id = t.Project.Id,
                        Name = t.Project.Name
                    },
                Developer = t.Developer == null ? null : new UserDto
                    {
                        Id = t.Developer.Id,
                        Name = t.Developer.Name
                    },
                Priority = t.Priority,
                Status = t.Status,
                Created = t.Created
            })
            .PaginateAsync<TicketDto>(request.pageNumber, request.pageSize, cancellationToken);
        }

        private async Task<List<UserDto>> GetSubmitterDtosAsync(TicketListRequest request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .ApplySecurityFilter(request.accessorRole, request.accessorId)
                .Select(s => new UserDto
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToListAsync(cancellationToken);
        }

        private async Task<List<ProjectDto>> GetProjectDtosAsync(TicketListRequest request, CancellationToken cancellationToken)
        {
            return await _context.Projects
                .ApplySecurityFilter(request.accessorRole, request.accessorId)
                .Select(p => new ProjectDto
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToListAsync(cancellationToken);
        }

        private async Task<List<UserDto>> GetDeveloperDtosAsync(TicketListRequest request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.Role != UserRole.Client)
                .ApplySecurityFilter(request.accessorRole, request.accessorId)
                .Select(d => new UserDto
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .ToListAsync(cancellationToken);
        }
    }
}