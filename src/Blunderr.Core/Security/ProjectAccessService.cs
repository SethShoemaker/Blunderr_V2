using Blunderr.Core.Data.Entities.Projects;
using Blunderr.Core.Data.Entities.Users;

namespace Blunderr.Core.Security
{
    public static class ProjectAccessService
    {
        public static bool CanViewProjects(UserRole userRole) => true;

        public static bool CanCreateProjects(UserRole userRole) => userRole == UserRole.Manager;

        public static bool CanEditProjects(UserRole userRole) => userRole == UserRole.Manager;

        public static bool CanDeleteProjects(UserRole userRole) => userRole == UserRole.Manager;

        public static IQueryable<Project> ApplySecurityFilter(this IQueryable<Project> queryable, UserRole userRole, int userId)
        {
            if(userRole == UserRole.Client)
                queryable = queryable.Where(p => p.Id == userId);

            return queryable;
        }
    }
}