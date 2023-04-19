using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Projects.ProjectCreate.GetCreateData
{
    public class GetCreateDataHandler : IRequestHandler<GetCreateDataRequest, GetCreateDataResponse>
    {
        private readonly AppDbContext _context;

        public GetCreateDataHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GetCreateDataResponse> Handle(GetCreateDataRequest request, CancellationToken cancellationToken)
        {
            GetCreateDataResponse r = new();

            if(!ProjectAccessService.CanCreateProjects(request.creatorRole)){
                r.Error = Error.Forbidden;
                return r;
            }

            r.Clients = await GetClientsAsync(request, cancellationToken);

            return r;
        }

        private async Task<List<ClientDto>> GetClientsAsync(GetCreateDataRequest request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.Role == UserRole.Client)
                .ApplySecurityFilter(request.creatorRole, request.creatorId)
                .Select(c => new ClientDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync(cancellationToken);
        }
    }
}