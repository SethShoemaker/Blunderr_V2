using Blunderr.Core.Services.PaginationService;

namespace Blunderr.Core.Features.Projects.ProjectList
{
    public class ProjectListResponse
    {
        public Page<ProjectDto> Page { get; set; } = null!;

        public List<ClientDto> Clients { get; set; } = null!;

        public bool CanManageProjects { get; set; }

        public bool CanViewUsers { get; set; }
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
}