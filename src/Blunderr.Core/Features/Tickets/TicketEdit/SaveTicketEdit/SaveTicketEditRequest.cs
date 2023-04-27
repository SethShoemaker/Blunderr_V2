using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Tickets.TicketEdit.SaveTicketEdit
{
    public class SaveTicketEditRequest : IRequest<SaveTicketEditResponse>
    {
        public UserRole EditorRole { get; set; }

        public int EditorId { get; set; }

        public int TicketId { get; set; }

        public string Title { get; set; } = null!;

        public int? DeveloperId { get; set; }

        public TicketType Type { get; set; }

        public TicketPriority Priority { get; set; }

        public string Description { get; set; } = null!;
    }
}