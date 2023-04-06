using Blunderr.Core.Data.Entities.FileItems;

namespace Blunderr.Core.Data.Entities.Tickets
{
    public class TicketAttachment : Entity
    {
        public FileItem FileItem { get; set; } = null!;

        public Ticket Ticket { get; set; } = null!;

        public int TicketId { get; set; }
    }
}