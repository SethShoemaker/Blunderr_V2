using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Entities.Users;

namespace Blunderr.Core.Data.Entities.Projects
{
    public class Project : Entity
    {
        public string Name { get; set; } = null!;

        public DateTime Created { get; set; }

        public User Client { get; set; } = null!;

        public int ClientId { get; set; }

        public List<Ticket> Tickets { get; set; } = null!;
    }
}