using Blunderr.Core.Data.Entities.FileItems;
using Blunderr.Core.Data.Entities.Projects;
using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Security;
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

            if(!TicketAccessService.CanCreateTickets(request.CreatorRole)){
                r.Errors.Add(SaveError.Forbidden);
                return r;
            }

            Project? project = await _context.Projects
                .Where(p => p.Id == request.ProjectId)
                .ApplySecurityFilter(request.CreatorRole, request.CreatorId)
                .FirstOrDefaultAsync(cancellationToken);

            if(project is null)
                r.Errors.Add(SaveError.ProjectNotFound);

            if(request.Title is null)
                r.Errors.Add(SaveError.TitleNull);

            if(request.Description is null)
                r.Errors.Add(SaveError.DescriptionNull);

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