using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Tickets.TicketDelete
{
    public class TicketDeleteHandler : IRequestHandler<TicketDeleteRequest, TicketDeleteResponse>
    {
        private readonly AppDbContext _context;

        public TicketDeleteHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TicketDeleteResponse> Handle(TicketDeleteRequest request, CancellationToken cancellationToken)
        {
            TicketDeleteResponse r = new();

            if(!TicketAccessService.CanDeleteTickets(request.deleterRole)){
                r.Error = Error.Forbidden;
                return r;
            }

            Ticket? ticket = await GetTicketAsync(request, cancellationToken);
            if(ticket is null){
                r.Error = Error.NotFound;
                return r;
            }

            foreach (var attachment in ticket.Attachments)
                _context.Remove(attachment.FileItem);

            foreach (var comment in ticket.Comments)
                foreach (var attachment in comment.Attachments)
                    _context.Remove(attachment.FileItem);

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return r;
        }

        private async Task<Ticket?> GetTicketAsync(TicketDeleteRequest request, CancellationToken cancellationToken)
        {
            return await _context.Tickets
                .Where(t => t.Id == request.TicketId)
                .ApplySecurityFilter(request.deleterRole, request.deleterId)
                .Include(t => t.Project)
                .Include(t => t.Attachments)
                .Include(t => t.Comments)
                .ThenInclude(tc => tc.Attachments)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}