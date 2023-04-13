using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Tickets.TicketDelete
{
    public record TicketDeleteRequest(
        UserRole deleterRole,
        int deleterId,
        int TicketId
    ) : IRequest<TicketDeleteResponse>;
}