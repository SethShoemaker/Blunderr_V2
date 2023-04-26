using Blunderr.Core.Features.Users.UserDelete;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blunderr.Web.Pages.Users
{
    [Authorize]
    public class Delete : PageModel
    {
        private readonly IMediator _mediator;

        public Delete(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FromRoute]
        public int UserId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            UserDeleteResponse response = await _mediator.Send(new UserDeleteRequest(User.Role(), User.Id(), UserId));

            if(response.Error == Error.Forbidden) return Forbid();
            if(response.Error == Error.NotFound) return BadRequest();

            return RedirectToPage("/Users/Index");
        }
    }
}