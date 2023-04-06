using Blunderr.Core.Data.Entities.FileItems;

namespace Blunderr.Core.Data.Entities.Tickets
{
    public class TicketCommentAttachment : Entity
    {
        public FileItem FileItem { get; set; } = null!;

        public TicketComment TicketComment { get; set; } = null!;

        public int TicketCommentId { get; set; }
    }
}