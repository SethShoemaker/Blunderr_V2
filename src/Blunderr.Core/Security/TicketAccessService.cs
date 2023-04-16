using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Entities.Users;

namespace Blunderr.Core.Security
{
    public static class TicketAccessService
    {
        public static bool CanListTickets(UserRole userRole) => true;

        public static bool CanShowTickets(UserRole userRole) => true;

        public static bool CanCreateTickets(UserRole userRole) => true;

        public static bool CanCreateTicketComments(UserRole userRole) => true;

        public static bool CanEditTickets(UserRole userRole) => userRole != UserRole.Client;

        public static bool CanDeleteTickets(UserRole userRole) => userRole != UserRole.Client;

        public static IQueryable<Ticket> ApplySecurityFilter(this IQueryable<Ticket> tickets, UserRole userRole, int userId)
        {
            if(userRole == UserRole.Client)
                tickets = tickets.Where(t => t.Project.ClientId == userId);
            else if(userRole == UserRole.Developer)
                tickets = tickets.Where(t => t.DeveloperId == userId);

            return tickets;
        }
    }
}