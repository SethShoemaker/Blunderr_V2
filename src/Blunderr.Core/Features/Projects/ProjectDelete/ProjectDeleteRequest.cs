using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Projects.ProjectDelete
{
    public record ProjectDeleteRequest(
        UserRole deleterRole,
        int deleterId,
        int projectId
    ) : IRequest<ProjectDeleteResponse>;
}