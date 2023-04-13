using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
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

            Ticket? ticket = await _context.Tickets
                .Where(t => t.Id == request.TicketId)
                .Include(t => t.Project)
                .Include(t => t.Attachments)
                .Include(t => t.Comments)
                .ThenInclude(tc => tc.Attachments)
                .FirstOrDefaultAsync();

            if(ticket is null){
                r.Error = Error.NotFound;
                return r;
            }

            if(request.deleterRole == UserRole.Client && ticket.Project.ClientId != request.deleterId){
                r.Error = Error.Forbidden;
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
    }
}