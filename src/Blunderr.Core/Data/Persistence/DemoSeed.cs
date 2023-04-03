using Blunderr.Core.Data.Entities.Projects;
using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Services.PasswordService;
using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Data.Persistence
{
    public static class DemoSeed
    {
        public static PasswordService _passwordService = new PasswordService();

        public static void Seed(this AppDbContext context, ModelBuilder modelBuilder)
        {
            SeedClients(modelBuilder);
            SeedDevelopers(modelBuilder);
            SeedManagers(modelBuilder);
        }

        private static void SeedClients(ModelBuilder modelBuilder)
        {
            (string client1Hash, string client1Salt) = _passwordService.GenerateHashAndSalt("Client1");
            User client1 = new User()
            {
                Id = 1,
                Name = "Sally Client",
                Email = "Sally@Client.com",
                Phone = 1234567890,
                PasswordHash = client1Hash,
                PasswordSalt = client1Salt,
                Role = UserRole.Client
            };
            modelBuilder.Entity<User>().HasData(client1);

            (string client2Hash, string Client2Salt) = _passwordService.GenerateHashAndSalt("Client2");
            User client2 = new User()
            {
                Id = 2,
                Name = "James Client",
                Email = "James@Client.com",
                Phone = 1234567890,
                PasswordHash = client2Hash,
                PasswordSalt = Client2Salt,
                Role = UserRole.Client
            };
            modelBuilder.Entity<User>().HasData(client2);

            (string client3Hash, string Client3Salt) = _passwordService.GenerateHashAndSalt("Client3");
            User client3 = new User()
            {
                Id = 3,
                Name = "Thomas Client",
                Email = "Thomas@Client.com",
                Phone = 1234567890,
                PasswordHash = client3Hash,
                PasswordSalt = Client3Salt,
                Role = UserRole.Client
            };
            modelBuilder.Entity<User>().HasData(client3);

            (string client4Hash, string Client4Salt) = _passwordService.GenerateHashAndSalt("Client4");
            User client4 = new User()
            {
                Id = 4,
                Name = "Lucas Client",
                Email = "Lucas@Client.com",
                Phone = 1234567890,
                PasswordHash = client4Hash,
                PasswordSalt = Client4Salt,
                Role = UserRole.Client
            };
            modelBuilder.Entity<User>().HasData(client4);


            Project client1project1 = new Project()
            {
                Id = 1,
                Name = "Sally's Website",
                Created = DateTime.Now,
                ClientId = 1
            };
            modelBuilder.Entity<Project>().HasData(client1project1);

            Project client1project2 = new Project()
            {
                Id = 2,
                Name = "Sally's Mobile App",
                Created = DateTime.Now,
                ClientId = 1
            };
            modelBuilder.Entity<Project>().HasData(client1project2);

            Project client2project1 = new Project()
            {
                Id = 3,
                Name = "James' Website",
                Created = DateTime.Now,
                ClientId = 2
            };
            modelBuilder.Entity<Project>().HasData(client2project1);

            Project client2project2 = new Project()
            {
                Id = 4,
                Name = "James' Mobile App",
                Created = DateTime.Now,
                ClientId = 2
            };
            modelBuilder.Entity<Project>().HasData(client2project2);

            Project client3project1 = new Project()
            {
                Id = 5,
                Name = "Thomas' Website",
                Created = DateTime.Now,
                ClientId = 3
            };
            modelBuilder.Entity<Project>().HasData(client3project1);

            Project client3project2 = new Project()
            {
                Id = 6,
                Name = "Thomas' Mobile App",
                Created = DateTime.Now,
                ClientId = 3
            };
            modelBuilder.Entity<Project>().HasData(client3project2);

            Project client4project1 = new Project()
            {
                Id = 7,
                Name = "Lucas' Website",
                Created = DateTime.Now,
                ClientId = 4
            };
            modelBuilder.Entity<Project>().HasData(client4project1);

            Project client4project2 = new Project()
            {
                Id = 8,
                Name = "Lucas' Mobile App",
                Created = DateTime.Now,
                ClientId = 4
            };
            modelBuilder.Entity<Project>().HasData(client4project2);

            Ticket client1project1ticket1 = new()
            {
                Id = 1,
                Title = "Ticket",
                Description = "I need help",
                ProjectId = client1project1.Id,
                SubmitterId = client1.Id,
                Status = TicketStatus.Pending,
                Type = TicketType.DocumentationRequest,
                Priority = TicketPriority.Low,
                Created = DateTime.Now
            };
            modelBuilder.Entity<Ticket>().HasData(client1project1ticket1);

            Ticket client1project1ticket2 = new()
            {
                Id = 2,
                Title = "Ticket 2",
                Description = "I need more help",
                ProjectId = client1project1.Id,
                SubmitterId = client1.Id,
                Status = TicketStatus.Pending,
                Type = TicketType.BugIssue,
                Priority = TicketPriority.Medium,
                Created = DateTime.Now
            };
            modelBuilder.Entity<Ticket>().HasData(client1project1ticket2);

            Ticket client1project1ticket3 = new()
            {
                Id = 3,
                Title = "Ticket 3",
                Description = "Could you build this?",
                ProjectId = client1project1.Id,
                SubmitterId = client1.Id,
                Status = TicketStatus.Pending,
                Type = TicketType.FeatureRequest,
                Priority = TicketPriority.Low,
                Created = DateTime.Now
            };
            modelBuilder.Entity<Ticket>().HasData(client1project1ticket3);
        }

        private static void SeedDevelopers(ModelBuilder modelBuilder)
        {
            (string Developer1Hash, string Developer1Salt) = _passwordService.GenerateHashAndSalt("Dev1");
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 5,
                    Name = "Jamie Dev",
                    Email = "Jamie@LoremIpsum.com",
                    Phone = 1234567890,
                    PasswordHash = Developer1Hash,
                    PasswordSalt = Developer1Salt,
                    Role = UserRole.Developer
                }
            );

            (string Developer2Hash, string Developer2Salt) = _passwordService.GenerateHashAndSalt("Dev2");
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 6,
                    Name = "Sam Dev",
                    Email = "Sam@LoremIpsum.com",
                    Phone = 1234567890,
                    PasswordHash = Developer2Hash,
                    PasswordSalt = Developer2Salt,
                    Role = UserRole.Developer
                }
            );
        }

        private static void SeedManagers(ModelBuilder modelBuilder)
        {
            (string Manager1Hash, string Manager1Salt) = _passwordService.GenerateHashAndSalt("Manager1");
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 7,
                    Name = "Jonathan Manager",
                    Email = "Jonathan@LoremIpsum.com",
                    Phone = 1234567890,
                    PasswordHash = Manager1Hash,
                    PasswordSalt = Manager1Salt,
                    Role = UserRole.Manager
                }
            );

            (string Manager2Hash, string Manager2Salt) = _passwordService.GenerateHashAndSalt("Manager2");
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 8,
                    Name = "Gerald Manager",
                    Email = "Gerald@LoremIpsum.com",
                    Phone = 1234567890,
                    PasswordHash = Manager2Hash,
                    PasswordSalt = Manager2Salt,
                    Role = UserRole.Manager
                }
            );
        }
    }
}