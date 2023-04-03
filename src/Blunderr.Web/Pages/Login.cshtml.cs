using Blunderr.Core.Features.Security.Login;
using Blunderr.Web.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blunderr.Web.Pages
{
    public class Login : PageModel
    {
        private readonly IMediator _mediator;

        public Login(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public string Email { get; set; } = null!;

        [BindProperty]
        public string Password { get; set; } = null!;

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid) 
                return Page();

            LoginResponse login = await _mediator.Send(new LoginRequest(Email, Password));
            if(!login.isSuccessful)
                return Page();

            Response.Cookies.Append(TokenAuthenticationSchemeOptions.CookieName, login.token);

            return RedirectToPage("/Index");
        }
    }
}