namespace Blunderr.Core.Data.Entities.Users
{
    public class User : Entity
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int? Phone { get; set; }

        public string PasswordSalt { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public UserRole Role { get; set; }
    }
}