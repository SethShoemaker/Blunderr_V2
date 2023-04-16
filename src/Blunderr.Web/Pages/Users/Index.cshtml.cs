using Blunderr.Core.Data.Entities.Users;
using Blunderr.Core.Features.Users.UserList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blunderr.Web.Pages.Users
{
    [Authorize(Roles="Developer,Manager")]
    public class Index : PageModel
    {
        private readonly IMediator _mediator;

        public Index(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FromQuery]
        public int PageNumber { get; set; } = 1;

        [FromQuery]
        public int PageSize { get; set; } = 10;

        [FromQuery]
        public UserRole? Role { get; set; }

        public UserListResponse Data { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync()
        {
            Data = await _mediator.Send(new UserListRequest(
                User.Role(),
                User.Id(),
                PageNumber,
                PageSize,
                Role
            ));

            return Data.Error == Error.Forbidden ? Forbid() : Page();
        }
    }
}