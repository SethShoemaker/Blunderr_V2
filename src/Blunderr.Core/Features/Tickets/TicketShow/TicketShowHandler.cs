using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Files.FileItemService;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Tickets.TicketShow
{
    public class TicketShowHandler : IRequestHandler<TicketShowRequest, TicketShowResponse>
    {
        private readonly DbSet<Ticket> _set;

        private readonly IFileItemService _fs;

        public TicketShowHandler(AppDbContext context, IFileItemService fileItemService)
        {
            _set = context.Tickets;
            _fs = fileItemService;
        }

        public async Task<TicketShowResponse> Handle(TicketShowRequest request, CancellationToken cancellationToken)
        {
            TicketShowResponse r = new();

            if(!TicketAccessService.CanShowTickets(request.accessorRole)){
                r.Error = Error.Forbidden;
                return r;
            }

            TicketDto? ticket = await GetTicketAsync(request, cancellationToken);
            if(ticket is null)
                r.Error = Error.NotFound;
            else
                r.Ticket = ticket;

            return r;
        }

        private async Task<TicketDto?> GetTicketAsync(TicketShowRequest request, CancellationToken cancellationToken)
        {
            return await _set
                .Where(t => t.Id == request.ticketId)
                .ApplySecurityFilter(request.accessorRole, request.accessorId)
                .Select(t => new TicketDto()
                {
                    Type = t.Type,
                    Priority = t.Priority,
                    Project = new ProjectDto
                    {
                        Id = t.Project.Id,
                        Name = t.Project.Name
                    },
                    Created = t.Created,
                    Title = t.Title,
                    Description = t.Description,
                    Attachments = t.Attachments.Select(a => new AttachmentDto
                    {
                        Location = _fs.LocationOf(a.FileItem),
                        FileName = a.FileItem.DisplayName
                    }).ToList(),
                    Status = t.Status,
                    Developer = t.Developer == null ? null : new UserDto { Id = t.Developer.Id, Name = t.Developer.Name },
                    Submitter = new UserDto { Id = t.Submitter.Id, Name = t.Submitter.Name },
                    Comments = t.Comments.Select(c => new TicketCommentDto
                    {
                        Submitter = new UserDto { Id = c.Submitter.Id, Name = c.Submitter.Name },
                        Created = c.Created,
                        Content = c.Content,
                        Attachments = c.Attachments.Select(a => new AttachmentDto
                        {
                            Location = _fs.LocationOf(a.FileItem),
                            FileName = a.FileItem.DisplayName
                        }).ToList()
                    }).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}