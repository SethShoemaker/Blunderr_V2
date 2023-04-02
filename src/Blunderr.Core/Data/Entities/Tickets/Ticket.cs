using Blunderr.Core.Data.Entities.Projects;
using Blunderr.Core.Data.Entities.Users;

namespace Blunderr.Core.Data.Entities.Tickets
{
    public class Ticket : Entity
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public Project Project { get; set; } = null!;

        public int ProjectId { get; set; }

        public User Submitter { get; set; } = null!;

        public int SubmitterId { get; set; }

        public User? Developer { get; set; } = null!;

        public int? DeveloperId { get; set; }

        public TicketStatus Status { get; set; }

        public TicketType Type { get; set; }

        public TicketPriority Priority { get; set; }

        public DateTime Created { get; set; }

        public List<TicketComment> Comments { get; set; } = null!;

        public List<TicketAttachment> Attachments { get; set; } = null!;
    }
}