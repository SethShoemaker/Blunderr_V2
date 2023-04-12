using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Projects.ProjectShow
{
    public record ProjectShowRequest(
        UserRole accessorRole,
        int accessorId,
        int projectId
    ) : IRequest<ProjectShowResponse>;
}