using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Files.FileItemService;
using Blunderr.Core.Data.Persistence;
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

            r.CanViewUsers = request.accessorRole != UserRole.Client;

            IQueryable<Ticket> ticket = _set.Where(t => t.Id == request.ticketId);

            if(request.accessorRole == UserRole.Client)
                ticket = ticket.Where(t => t.Project.ClientId == request.accessorId);

            r.Ticket = await (
                from t in ticket
                select new TicketDto
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
                    Attachments = (
                            from a in t.Attachments 
                            select new AttachmentDto 
                            { 
                                Location = _fs.LocationOf(a.FileItem),
                                FileName = a.FileItem.DisplayName
                            }
                        ).ToList(),
                    Status = t.Status,
                    Developer = t.Developer == null ? null : new UserDto { Id = t.Developer.Id, Name = t.Developer.Name },
                    Submitter = new UserDto { Id = t.Submitter.Id, Name = t.Submitter.Name },
                    Comments = (
                            from c in t.Comments
                            select new TicketCommentDto
                            {
                                Submitter = new UserDto{ Id = c.Submitter.Id, Name = c.Submitter.Name },
                                Created = c.Created,
                                Content = c.Content,
                                Attachments = (
                                        from a in c.Attachments 
                                        select new AttachmentDto 
                                        { 
                                            Location = _fs.LocationOf(a.FileItem),
                                            FileName = a.FileItem.DisplayName
                                        }
                                    ).ToList()
                            }
                        ).ToList()
                }).FirstOrDefaultAsync();

            return r;
        }
    }
}