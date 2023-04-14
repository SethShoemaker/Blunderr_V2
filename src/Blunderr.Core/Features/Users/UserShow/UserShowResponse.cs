using Blunderr.Core.Data.Entities.Users;

namespace Blunderr.Core.Features.Users.UserShow
{
    public class UserShowResponse
    {
        public UserDto User { get; set; } = null!;

        public Error? Error { get; set; }

        public bool CanManageUser { get; set; }

        public bool isSuccessful() => Error is null;
    }

    public class UserDto
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public UserRole Role { get; set; }

        public int NumSubmittedTickets { get; set; }

        public bool CanBeAssignedTickets { get; set; }

        public int NumAssignedTickets { get; set; }
    }

    public enum Error
    {
        NotFound,
        Forbidden
    }
}