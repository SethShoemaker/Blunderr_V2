using Blunderr.Core.Data.Entities.Users;

namespace Blunderr.Core.Data.Entities.Tickets
{
    public class TicketComment : Entity
    {
        public string Content { get; set; } = null!;

        public User Submitter { get; set; } = null!;

        public int SubmitterId { get; set; }

        public DateTime Created { get; set; }

        public List<TicketCommentAttachment> Attachments { get; set; } = null!;
    }
}