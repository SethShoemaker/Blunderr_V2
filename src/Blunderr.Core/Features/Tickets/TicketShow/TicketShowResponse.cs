using Blunderr.Core.Data.Entities.Tickets;

namespace Blunderr.Core.Features.Tickets.TicketShow
{
    public class TicketShowResponse
    {
        public TicketDto Ticket { get; set; } = null!;

        public Error? Error { get; set; }

        public bool IsSuccessful() => Error is null;
    }

    public class TicketDto
    {
        public TicketType Type { get; set; }

        public TicketPriority Priority { get; set; }

        public ProjectDto Project { get; set; } = null!;

        public DateTime Created { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public List<AttachmentDto> Attachments { get; set; } = null!;

        public TicketStatus Status { get; set; }

        public UserDto? Developer { get; set; } = null!;

        public UserDto Submitter { get; set; } = null!;

        public List<TicketCommentDto> Comments { get; set; } = null!;
    }

    public class ProjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }

    public class AttachmentDto
    {
        public string Location { get; set; } = null!;

        public string FileName { get; set; } = null!;
    }

    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }

    public class TicketCommentDto
    {
        public UserDto Submitter { get; set; } = null!;

        public DateTime Created { get; set; }

        public string Content { get; set; } = null!;

        public List<AttachmentDto> Attachments { get; set; } = null!;
    }

    public enum Error
    {
        Forbidden,
        NotFound
    }
}