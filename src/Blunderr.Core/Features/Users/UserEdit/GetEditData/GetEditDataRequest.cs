using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Users.UserEdit.GetEditData
{
    public record GetEditDataRequest(
        UserRole editorRole,
        int editorId,
        int userId
    ) : IRequest<GetEditDataResponse>;
}