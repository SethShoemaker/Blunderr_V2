namespace Blunderr.Core.Features.Tickets.TicketCommentCreate
{
    public class TicketCommentCreateResponse
    {
        public int TicketCommentId { get; set; }

        public SaveError? Error { get; set; }

        public bool isSuccessful() => Error is null;
    }

    public enum SaveError
    {
        Forbidden,
        TicketNotFound
    }
}