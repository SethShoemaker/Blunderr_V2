namespace Blunderr.Core.Features.Projects.ProjectShow
{
    public class ProjectShowResponse
    {
        public ProjectDto? Project { get; set; } = null!;

        public Error? Error { get; set; }

        public bool isSuccessful() => Error is null;
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

    public enum Error
    {
        Forbidden,
        NotFound
    }
}