using Blunderr.Core.Data.Entities.Projects;
using Blunderr.Core.Data.Persistence;
using Blunderr.Core.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Features.Projects.ProjectDelete
{
    public class ProjectDeleteHandler : IRequestHandler<ProjectDeleteRequest, ProjectDeleteResponse>
    {
        private readonly AppDbContext _context;

        public ProjectDeleteHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectDeleteResponse> Handle(ProjectDeleteRequest request, CancellationToken cancellationToken)
        {
            ProjectDeleteResponse r = new();

            if(!ProjectAccessService.CanDeleteProjects(request.deleterRole)){
                r.Error = Error.Forbidden;
                return r;
            }

            IQueryable<Project> projectQuery = _context.Projects
                .Where(p => p.Id == request.projectId)
                .ApplySecurityFilter(request.deleterRole, request.deleterId)
                .Include(p => p.Tickets)
                    .ThenInclude(t => t.Attachments)
                .Include(p => p.Tickets)
                    .ThenInclude(t => t.Comments)
                        .ThenInclude(tc => tc.Attachments);

            Project? project = await projectQuery.FirstOrDefaultAsync();
            if(project is null){
                r.Error = Error.NotFound;
                return r;
            }

            foreach (var ticket in project.Tickets)
            {
                foreach (var attachment in ticket.Attachments)
                    _context.Remove(attachment.FileItem);

                foreach (var comment in ticket.Comments)
                    foreach (var attachment in comment.Attachments)
                        _context.Remove(attachment.FileItem);
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return r;
        }
    }
}