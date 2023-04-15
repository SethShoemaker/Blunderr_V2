namespace Blunderr.Core.Features.Tickets.TicketCreate.Query
{
    public class GetCreateDataResponse
    {
        public List<ProjectDto> Projects { get; set; } = null!;
    }

    public class ProjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}