using Blunderr.Core.Data.Entities.Projects;
using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<UserToken> UserTokens { get; set; } = null!;

        public DbSet<Project> Projects { get; set; } = null!;

        public DbSet<Ticket> Tickets { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserToken>().HasOne(ut => ut.User);

            modelBuilder.Entity<Ticket>().HasOne(t => t.Developer);
            modelBuilder.Entity<Ticket>().HasOne(t => t.Submitter);
        }
    }
}