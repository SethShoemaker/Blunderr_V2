using Blunderr.Core.Data.Entities.Users;

namespace Blunderr.Core.Security
{
    public static class TicketAccessService
    {
        public static bool CanViewTickets(UserRole userRole) => true;

        public static bool CanEditTickets(UserRole userRole) => userRole != UserRole.Client;

        public static bool CanCreateTickets(UserRole userRole) => true;

        public static bool CanDeleteTickets(UserRole userRole) => userRole != UserRole.Client;
    }
}