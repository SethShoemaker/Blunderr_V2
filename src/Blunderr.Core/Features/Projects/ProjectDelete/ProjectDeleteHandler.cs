using Blunderr.Core.Data.Entities.Projects;
using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Persistence;
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

            IQueryable<Project> projectQuery = _context.Projects
                .Where(p => p.Id == request.projectId)
                .Include(p => p.Tickets)
                    .ThenInclude(t => t.Attachments)
                .Include(p => p.Tickets)
                    .ThenInclude(t => t.Comments)
                        .ThenInclude(tc => tc.Attachments);

            if(request.deleterRole == UserRole.Client)
                projectQuery = projectQuery.Where(p => p.ClientId == request.deleterId);
            else if(request.deleterRole == UserRole.Developer){
                r.Error = Error.Forbidden;
                return r;
            }

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