using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Users.UserCreate
{
    public class UserCreateRequest  : IRequest<UserCreateResponse>
    {
        public UserRole CreatorRole { get; set; }

        public int CreatorId { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int? Phone { get; set; }

        public string Password { get; set; } = null!;

        public UserRole Role { get; set; }
    }
}