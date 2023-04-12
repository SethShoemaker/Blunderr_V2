namespace Blunderr.Core.Features.Projects.ProjectShow
{
    public class ProjectShowResponse
    {
        public ProjectDto? Project { get; set; } = null!;

        public bool CanManageProject { get; set; }

        public bool CanViewUsers { get; set; }

        public bool isSuccessful() => Project != null;
    }

    public class ProjectDto
    {
        public string Name { get; set; } = null!;

        public ClientDto Client { get; set; } = null!;

        public DateTime Created { get; set; }

        public int NumPendingTickets { get; set; }

        public int NumOpenTickets { get; set; }

        public int NumResolvedTickets { get; set; }
    }

    public class ClientDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}