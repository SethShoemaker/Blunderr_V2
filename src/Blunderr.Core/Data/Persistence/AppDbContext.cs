using Blunderr.Core.Data.Entities.FileItems;
using Blunderr.Core.Data.Entities.Projects;
using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Data.Files.FileItemService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Blunderr.Core.Data.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options, IFileItemService fileItemService) : base(options)
        {
            FileItemService = fileItemService;
        }
        
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<UserToken> UserTokens { get; set; } = null!;

        public DbSet<Project> Projects { get; set; } = null!;

        public DbSet<Ticket> Tickets { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserToken>().HasOne(ut => ut.User);

            modelBuilder.Entity<Ticket>().HasOne(t => t.Developer);
            modelBuilder.Entity<Ticket>().HasOne(t => t.Submitter);

            modelBuilder.Entity<TicketAttachment>().Navigation(ta => ta.FileItem).AutoInclude();
            modelBuilder.Entity<TicketCommentAttachment>().Navigation(tca => tca.FileItem).AutoInclude();

            this.Seed(modelBuilder);
        }

        private IFileItemService FileItemService { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await FileItemService.HandleFileItemEntriesAsync(ChangeTracker.Entries<FileItem>().AsQueryable());

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}