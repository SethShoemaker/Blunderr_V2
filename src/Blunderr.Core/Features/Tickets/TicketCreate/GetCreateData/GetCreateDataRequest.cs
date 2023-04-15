using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Tickets.TicketCreate.Query
{
    public record GetCreateDataRequest(
        UserRole creatorRole,
        int creatorId
    ) : IRequest<GetCreateDataResponse>;
}