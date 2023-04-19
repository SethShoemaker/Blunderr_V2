using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Tickets.TicketEdit.GetEditData
{
    public record GetEditDataRequest(
        UserRole editorRole,
        int editorId,
        int ticketId
    ) : IRequest<GetEditDataResponse>;
}