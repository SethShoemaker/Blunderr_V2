using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Projects.ProjectEdit.GetEditData
{
    public record GetEditDataRequest(
        UserRole editorRole,
        int editorId,
        int projectId
    ): IRequest<GetEditDataResponse>;
}