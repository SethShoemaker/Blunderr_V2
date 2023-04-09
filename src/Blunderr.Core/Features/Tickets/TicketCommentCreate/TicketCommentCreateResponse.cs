namespace Blunderr.Core.Features.Tickets.TicketCommentCreate
{
    public class TicketCommentCreateResponse
    {
        public int? TicketCommentId { get; set; }

        public bool isSuccessful() => TicketCommentId != null;
    }
}