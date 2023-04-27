using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Features.Tickets.TicketEdit.SaveTicketEdit;
using Blunderr.Core.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Tickets.TicketEdit.GetEditData
{
    public class GetEditDataHandler : IRequestHandler<GetEditDataRequest, GetEditDataResponse>
    {
        private readonly AppDbContext _context;

        public GetEditDataHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GetEditDataResponse> Handle(GetEditDataRequest request, CancellationToken cancellationToken)
        {
            GetEditDataResponse r = new();

            if(!TicketAccessService.CanEditTickets(request.editorRole)){
                r.Error = Error.Forbidden;
                return r;
            }

            SaveTicketEditRequest? saveRequest = await GetSaveTicketEditRequestAsync(request, cancellationToken);
            if(saveRequest is null){
                r.Error = Error.NotFound;
                return r;
            }

            r.SaveRequest = saveRequest;
            r.Developers = await GetDevelopersAsync(request, cancellationToken);

            return r;
        }

        private async Task<SaveTicketEditRequest?> GetSaveTicketEditRequestAsync(GetEditDataRequest request, CancellationToken cancellationToken)
        {
            return await _context.Tickets
                .Where(t => t.Id == request.ticketId)
                .ApplySecurityFilter(request.editorRole, request.editorId)
                .Select(t => new SaveTicketEditRequest()
                {
                    Title = t.Title,
                    Type = t.Type,
                    Priority = t.Priority,
                    Description = t.Description,
                    DeveloperId = t.DeveloperId
                }).FirstOrDefaultAsync(cancellationToken);
        }

        private async Task<List<DeveloperDto>> GetDevelopersAsync(GetEditDataRequest request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.Role != UserRole.Client)
                .ApplySecurityFilter(request.editorRole, request.editorId)
                .Select(u => new DeveloperDto
                {
                    Id = u.Id,
                    Name = u.Name
                })
                .ToListAsync(cancellationToken);
        }
    }
}