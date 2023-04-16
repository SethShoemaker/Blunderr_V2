using Blunderr.Core.Services.PaginationService;

namespace Blunderr.Core.Features.Projects.ProjectList
{
    public class ProjectListResponse
    {
        public Page<ProjectDto> Page { get; set; } = null!;

        public List<ClientDto> Clients { get; set; } = null!;

        public Error? Error { get; set; }

        public bool isSuccessful() => Error is null;
    }

    public class ProjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ClientDto Client { get; set; } = null!;

        public DateTime Created { get; set; }
    }

    public class ClientDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }

    public enum Error
    {
        Forbidden
    }
}