using Blunderr.Core.Data.Entities.Users;
using MediatR;

namespace Blunderr.Core.Features.Users.UserEdit.SaveEditData
{
    public class SaveEditDataRequest : IRequest<SaveEditDataResponse>
    {
        public UserRole EditorRole { get; set; }

        public int EditorId { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int? Phone { get; set; }

        public string Password { get; set; } = null!;
    }
}