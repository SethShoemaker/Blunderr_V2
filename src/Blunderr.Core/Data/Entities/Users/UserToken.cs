namespace Blunderr.Core.Data.Entities.Users
{
    public class UserToken : Entity
    {
        public string Value { get; set; } = null!;

        public User User { get; set; } = null!;

        public int UserId { get; set; }
    }
}