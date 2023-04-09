using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Tickets.TicketCommentCreate
{
    public record TicketCommentCreateRequest(
        UserRole submitterRole,
        int SubmitterId,
        int ticketId,
        string content,
        IEnumerable<FileItemDto> fileItemDtos
    ) : IRequest<TicketCommentCreateResponse>;

    public record FileItemDto(string displayName, Stream fileStream);
}