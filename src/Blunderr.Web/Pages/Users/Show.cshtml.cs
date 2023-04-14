using Blunderr.Core.Features.Users.UserShow;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blunderr.Web.Pages.Users
{
    [Authorize(Roles="Developer,Manager")]
    public class Show : PageModel
    {
        private readonly IMediator _mediator;

        public Show(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FromRoute]
        public int UserId { get; set; }

        public UserShowResponse Data { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync()
        {
            Data = await _mediator.Send(new UserShowRequest(User.Role(), UserId));

            if(Data.Error == Error.NotFound) return NotFound();
            else return Page();
        }
    }
}