using Blunderr.Core.Features.Projects.ProjectCreate.SaveCreateData;

namespace Blunderr.Core.Features.Projects.ProjectCreate.GetCreateData
{
    public class GetCreateDataResponse
    {
        public List<ClientDto> Clients { get; set; } = null!;

        public Error? Error { get; set; }
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