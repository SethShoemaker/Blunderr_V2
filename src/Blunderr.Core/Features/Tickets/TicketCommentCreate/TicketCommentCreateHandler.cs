using Blunderr.Core.Data.Entities.FileItems;
using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Security;
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

            if(!TicketAccessService.CanCreateTicketComments(request.submitterRole)){
                r.Error = SaveError.Forbidden;
                return r;
            }

            Ticket? ticket = await GetTicketAsync(request, cancellationToken);
            if(ticket is null){
                r.Error = SaveError.TicketNotFound;
                return r;
            }

            TicketComment comment = CreateComment(request, cancellationToken);

            ticket.Comments.Add(comment);
            await _context.SaveChangesAsync();

            r.TicketCommentId = comment.Id;
            return r;
        }

        private async Task<Ticket?> GetTicketAsync(TicketCommentCreateRequest request, CancellationToken cancellationToken)
        {
            return await _context.Tickets
                .Where(t => t.Id == request.ticketId)
                .ApplySecurityFilter(request.submitterRole, request.SubmitterId)
                .Include(t => t.Comments)
                .FirstOrDefaultAsync(cancellationToken);
        }

        private TicketComment CreateComment(TicketCommentCreateRequest request, CancellationToken cancellationToken)
        {
            TicketComment comment = new()
            {
                Content = request.content,
                SubmitterId = request.SubmitterId,
                Created = DateTime.Now
            };

            comment.Attachments = request.fileItemDtos
            .Select(fi => new TicketCommentAttachment()
            {
                FileItem = new FileItem
                    {
                        FileStream = fi.fileStream,
                        DisplayName = fi.displayName
                    },
                TicketComment = comment
            }).ToList();

            return comment;
        }
    }
}