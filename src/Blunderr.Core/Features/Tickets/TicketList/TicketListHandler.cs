using Blunderr.Core.Data.Entities.Projects;
using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
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
            return new TicketListResponse()
            {
                Page = await GetTicketDtosAsync(request, cancellationToken),
                Submitters = await GetSubmitterDtosAsync(request, cancellationToken),
                Projects = await GetProjectDtosAsync(request, cancellationToken),
                Developers = await GetDeveloperDtosAsync(request, cancellationToken),
                CanManageTickets = request.accessorRole != UserRole.Client
            };
        }

        private async Task<Page<TicketDto>> GetTicketDtosAsync(TicketListRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Ticket> tickets = _context.Tickets;

            if(request.accessorRole == UserRole.Client)
                tickets = tickets.Where(t => t.Project.ClientId == request.accessorId);
            else if(request.accessorRole == UserRole.Developer)
                tickets = tickets.Where(t => t.DeveloperId == request.accessorId);

            if(request.submitterId is not null)
                tickets = tickets.Where(t => t.SubmitterId == request.submitterId);

            if(request.projectId is not null)
                tickets = tickets.Where(t => t.ProjectId == request.projectId);

            if(request.developerId is not null)
                tickets = tickets.Where(t => t.DeveloperId == request.developerId);

            if(request.ticketStatus is not null)
                tickets = tickets.Where(t => t.Status == request.ticketStatus);

            IQueryable<TicketDto> ticketDtos = tickets.Select(t => new TicketDto
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
            });

            return await ticketDtos.PaginateAsync<TicketDto>(request.pageNumber, request.pageSize, cancellationToken);
        }

        private async Task<List<UserDto>> GetSubmitterDtosAsync(TicketListRequest request, CancellationToken cancellationToken)
        {
            IQueryable<User> submitters = _context.Users;

            if(request.accessorRole == UserRole.Client) // filter out other clients
                submitters = submitters.Where(u => u.Role != UserRole.Developer || u.Id == request.accessorId);

            IQueryable<UserDto> submitterDtos = submitters.Select(s => new UserDto
            {
                Id = s.Id,
                Name = s.Name
            });

            return await submitterDtos.ToListAsync();
        }

        private async Task<List<ProjectDto>> GetProjectDtosAsync(TicketListRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Project> projects = _context.Projects;

            if(request.accessorRole == UserRole.Client)
                projects = projects.Where(p => p.ClientId == request.accessorId);

            IQueryable<ProjectDto> projectDtos = projects.Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name
            });

            return await projectDtos.ToListAsync();
        }

        private async Task<List<UserDto>> GetDeveloperDtosAsync(TicketListRequest request, CancellationToken cancellationToken)
        {
            IQueryable<User> developers = _context.Users.Where(u => u.Role != UserRole.Client);

            IQueryable<UserDto> developerDtos = developers.Select(d => new UserDto
            {
                Id = d.Id,
                Name = d.Name
            });

            return await developerDtos.ToListAsync();
        }
    }
}