using Blunderr.Core.Data.Entities.Users;

namespace Blunderr.Core.Security
{
    public static class UserAccessService
    {
        public static bool CanViewUsers(UserRole userRole) => userRole != UserRole.Client;

        public static bool CanCreateUsers(UserRole userRole) => userRole == UserRole.Manager;

        public static bool CanEditUsers(UserRole userRole) => userRole == UserRole.Manager;

        public static bool CanDeleteUsers(UserRole userRole) => userRole == UserRole.Manager;
    }
}