using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Services.PaginationService;

namespace Blunderr.Core.Features.Tickets.TicketList
{
    public class TicketListResponse
    {
        public Page<TicketDto> Page { get; set; } = null!;

        public List<UserDto> Submitters { get; set; } = null!;

        public List<ProjectDto> Projects { get; set; } = null!;

        public List<UserDto> Developers { get; set; } = null!;

        public Error? Error { get; set; }

        public bool IsSuccessful() => Error is null;
    }

    public class TicketDto
    {
        public int Id { get; set; }

        public UserDto Submitter { get; set; } = null!;

        public ProjectDto Project { get; set; } = null!;

        public UserDto? Developer { get; set; }

        public TicketPriority Priority { get; set; }

        public TicketStatus Status { get; set; }

        public DateTime Created { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }

    public class ProjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }

    public enum Error
    {
        Forbidden
    }
}