using Blunderr.Core.Features.Users.UserCreate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blunderr.Web.Pages.Users
{
    [Authorize]
    public class Create : PageModel
    {
        private readonly IMediator _mediator;

        public Create(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public UserCreateRequest CreateRequest { get; set; } = null!;

        public async Task<IActionResult> OnPostAsync()
        {
            CreateRequest.CreatorRole = User.Role();
            CreateRequest.CreatorId = User.Id();

            UserCreateResponse createResponse = await _mediator.Send(CreateRequest);

            if(createResponse.Error == Error.Forbidden) return Forbid();

            return RedirectToPage("/Users/Show", new { UserId = createResponse.UserId });
        }
    }
}