using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Projects.ProjectEdit.SaveEditData
{
    public record SaveEditDataRequest : IRequest<SaveEditDataResponse>
    {
        public UserRole EditorRole { get; set; }

        public int EditorId { get; set; }

        public int ProjectId { get; set; }

        public string Name { get; set; } = null!;
    }
}