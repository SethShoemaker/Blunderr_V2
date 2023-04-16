using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Services.PaginationService;

namespace Blunderr.Core.Features.Users.UserList
{
    public class UserListResponse
    {
        public Page<UserDto>? Page { get; set; }

        public Error? Error { get; set; }

        public bool IsSuccessful() => Error is null;
    }

    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public UserRole Role { get; set; }

        public bool CanBeAssignedTickets { get; set; }
    }

    public enum Error
    {
        Forbidden
    }
}