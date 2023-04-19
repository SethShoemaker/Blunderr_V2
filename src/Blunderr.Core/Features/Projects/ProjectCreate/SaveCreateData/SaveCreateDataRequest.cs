using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Projects.ProjectCreate.SaveCreateData
{
    public class SaveCreateDataRequest : IRequest<SaveCreateDataResponse>
    {
        public UserRole CreatorRole { get; set; }

        public int CreatorId { get; set; }

        public string ProjectName { get; set; } = null!;

        public int ClientId { get; set; }
    }
}