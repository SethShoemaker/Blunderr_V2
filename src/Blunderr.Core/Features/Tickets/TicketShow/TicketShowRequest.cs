using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Tickets.TicketShow
{
    public record TicketShowRequest(UserRole accessorRole, int accessorId, int ticketId) : IRequest<TicketShowResponse>;
}