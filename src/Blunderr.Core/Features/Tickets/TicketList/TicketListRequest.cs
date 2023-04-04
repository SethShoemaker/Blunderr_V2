using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Tickets.TicketList
{
    public record TicketListRequest(
        UserRole accessorRole, 
        int accessorId, 
        int pageNumber,
        int pageSize,
        int? submitterId,
        int? projectId,
        int? developerId,
        TicketStatus? ticketStatus
    ) : IRequest<TicketListResponse>;
}