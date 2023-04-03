namespace Blunderr.Core.Data.Entities.Tickets
{
    public class TicketCommentAttachment : Entity
    {
        public string FileName { get; set; } = null!;

        public TicketComment TicketComment { get; set; } = null!;

        public int TicketCommentId { get; set; }
    }
}