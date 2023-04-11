using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Projects.ProjectList
{
    public record ProjectListRequest(
        UserRole accessorRole,
        int accessorId,
        int pageNumber,
        int pageSize,
        int? clientId
    ) : IRequest<ProjectListResponse>;
}