using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Tickets.TicketCreate.SaveTicket
{
    public class SaveTicketRequest : IRequest<SaveTicketResponse>
    {
        public UserRole CreatorRole { get; set; }

        public int CreatorId { get; set; }

        public string Title { get; set; } = null!;

        public TicketType Type { get; set; }

        public int ProjectId { get; set; }

        public TicketPriority Priority { get; set; }

        public string Description { get; set; } = null!;

        public List<AttachmentDto> Attachments { get; set; } = null!;
    }

    public class AttachmentDto
    {
        public AttachmentDto(Stream fileStream, string fileName)
        {
            FileStream = fileStream;
            FileName = fileName;
        }

        public Stream FileStream { get; set; } = null!;

        public string FileName { get; set; } = null!;
    }
}