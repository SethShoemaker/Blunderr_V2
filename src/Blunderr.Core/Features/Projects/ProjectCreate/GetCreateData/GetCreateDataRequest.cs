using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Projects.ProjectCreate.GetCreateData
{
    public record GetCreateDataRequest(
        UserRole creatorRole,
        int creatorId
    ) : IRequest<GetCreateDataResponse>;
}