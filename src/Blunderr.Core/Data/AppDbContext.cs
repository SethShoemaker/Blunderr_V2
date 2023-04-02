using Blunderr.Core.Data.Entities.Projects;
using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        DbSet<Project> Projects { get; set; } = null!;

        DbSet<Ticket> Tickets { get; set; } = null!;
    }
}