using Blunderr.Core.Data.Entities.Users;

namespace Blunderr.Core.Data.Entities.Tickets
{
    public class TicketComment : Entity
    {
        public Ticket Ticket { get; set; } = null!;

        public int TicketId { get; set; }
        
        public string Content { get; set; } = null!;

        public User Submitter { get; set; } = null!;

        public int SubmitterId { get; set; }

        public DateTime Created { get; set; }

        public List<TicketCommentAttachment> Attachments { get; set; } = null!;
    }
}