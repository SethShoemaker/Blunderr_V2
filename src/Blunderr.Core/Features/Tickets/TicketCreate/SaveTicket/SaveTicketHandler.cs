using Blunderr.Core.Data.Entities.FileItems;
using Blunderr.Core.Data.Entities.Projects;
using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Tickets.TicketCreate.SaveTicket
{
    public class SaveTicketHandler : IRequestHandler<SaveTicketRequest, SaveTicketResponse>
    {
        private readonly AppDbContext _context;

        public SaveTicketHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SaveTicketResponse> Handle(SaveTicketRequest request, CancellationToken cancellationToken)
        {
            SaveTicketResponse r = new();

            Project? project = await _context.Projects
                .Where(p => p.Id == request.ProjectId)
                .FirstOrDefaultAsync(cancellationToken);

            if(project is not null && request.CreatorRole is UserRole.Client && project.ClientId != request.CreatorId){
                r.Errors.Add(Error.Forbidden);
                return r;
            }

            if(project is null)
                r.Errors.Add(Error.ProjectNotFound);

            if(request.Title is null)
                r.Errors.Add(Error.TitleNull);

            if(request.Description is null)
                r.Errors.Add(Error.DescriptionNull);

            if(r.Errors.Any())
                return r;

            Ticket ticket = new(){
                Title = request.Title,
                Description = request.Description,
                Project = project,
                SubmitterId = request.CreatorId,
                Status = TicketStatus.Pending,
                Type = request.Type,
                Priority = request.Priority,
                Created = DateTime.Now
            };

            ticket.Attachments = request.Attachments.Select(a => new TicketAttachment()
            {
                FileItem = new FileItem()
                    {
                        FileStream = a.FileStream,
                        FileName = a.FileName,
                        DisplayName = a.FileName
                    },
                Ticket = ticket
            }).ToList();

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync(cancellationToken);

            r.TicketId = ticket.Id;
            return r;
        }
    }
}