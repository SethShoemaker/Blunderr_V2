namespace Blunderr.Core.Data.Entities.Tickets
{
    public class TicketAttachment : Entity
    {
        public string FileName { get; set; } = null!;

        public Ticket Ticket { get; set; } = null!;

        public int TicketId { get; set; }
    }
}