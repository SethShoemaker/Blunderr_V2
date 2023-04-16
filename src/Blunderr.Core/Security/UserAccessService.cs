using Blunderr.Core.Data.Entities.Users;

namespace Blunderr.Core.Security
{
    public static class UserAccessService
    {
        public static bool CanListUsers(UserRole userRole) => userRole != UserRole.Client;

        public static bool CanShowUsers(UserRole userRole) => userRole != UserRole.Client;

        public static bool CanCreateUsers(UserRole userRole) => userRole == UserRole.Manager;

        public static bool CanEditUsers(UserRole userRole) => userRole == UserRole.Manager;

        public static bool CanDeleteUsers(UserRole userRole) => userRole == UserRole.Manager;

        public static IQueryable<User> ApplySecurityFilter(this IQueryable<User> users, UserRole userRole, int userId)
        {
            if(userRole == UserRole.Client) // filter out other clients
                users = users.Where(u => u.Role != UserRole.Client || u.Id == userId);

            return users;
        }
    }
}