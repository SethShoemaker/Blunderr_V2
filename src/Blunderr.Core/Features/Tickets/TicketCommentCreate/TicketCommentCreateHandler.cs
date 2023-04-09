using Blunderr.Core.Data.Entities.FileItems;
using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Tickets.TicketCommentCreate
{
    public class TicketCommentCreateHandler : IRequestHandler<TicketCommentCreateRequest, TicketCommentCreateResponse>
    {
        private readonly AppDbContext _context;
        public TicketCommentCreateHandler(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<TicketCommentCreateResponse> Handle(TicketCommentCreateRequest request, CancellationToken cancellationToken)
        {
            TicketCommentCreateResponse r = new();

            Ticket? ticket = await GetTicketAsync(request, cancellationToken);
            if(ticket is null)
                return r;

            TicketComment comment = CreateComment(request, cancellationToken);

            ticket.Comments.Add(comment);
            await _context.SaveChangesAsync();

            r.TicketCommentId = comment.Id;
            return r;
        }

        private async Task<Ticket?> GetTicketAsync(TicketCommentCreateRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Ticket> ticketQuery = _context.Tickets
                .Where(t => t.Id == request.ticketId)
                .Include(t => t.Comments);

            if(request.submitterRole == UserRole.Client)
                ticketQuery = ticketQuery.Where(t => t.Project.ClientId == request.SubmitterId);

            return await ticketQuery.FirstOrDefaultAsync(cancellationToken);
        }

        private TicketComment CreateComment(TicketCommentCreateRequest request, CancellationToken cancellationToken)
        {
            TicketComment comment = new()
            {
                Content = request.content,
                SubmitterId = request.SubmitterId,
                Created = DateTime.Now
            };

            comment.Attachments = (
                from f in request.fileItemDtos
                select new TicketCommentAttachment
                {
                    FileItem = new FileItem 
                        { 
                            FileStream = f.fileStream,
                            DisplayName = f.displayName
                        },
                    TicketComment = comment
                }
            ).ToList();

            return comment;
        }
    }
}