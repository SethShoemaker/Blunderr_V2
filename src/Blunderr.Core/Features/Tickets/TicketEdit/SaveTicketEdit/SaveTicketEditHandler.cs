using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Tickets.TicketEdit.SaveTicketEdit
{
    public class SaveTicketEditHandler : IRequestHandler<SaveTicketEditRequest, SaveTicketEditResponse>
    {
        private readonly AppDbContext _context;

        public SaveTicketEditHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SaveTicketEditResponse> Handle(SaveTicketEditRequest request, CancellationToken cancellationToken)
        {
            SaveTicketEditResponse r = new();

            if(!TicketAccessService.CanEditTickets(request.EditorRole)){
                r.Error = SaveError.Forbidden;
                return r;
            }

            Ticket? ticket = await GetTicketAsync(request, cancellationToken);
            if(ticket is null){
                r.Error = SaveError.NotFound;
                return r;
            }

            ticket.Title = request.Title;
            ticket.Type = request.Type;
            ticket.Priority = request.Priority;
            ticket.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);
            return r;
        }

        private async Task<Ticket?> GetTicketAsync(SaveTicketEditRequest request, CancellationToken cancellationToken)
        {
            return await _context.Tickets
                .Where(t => t.Id == request.TicketId)
                .ApplySecurityFilter(request.EditorRole, request.EditorId)
                .Include(t => t.Project)
                .Include(t => t.Attachments)
                .ThenInclude(ta => ta.FileItem)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}