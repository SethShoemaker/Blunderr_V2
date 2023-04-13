using Blunderr.Web.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blunderr.Web.Pages
{
    [Authorize]
    public class Logout : PageModel
    {
        public IActionResult OnGet()
        {
            Response.Cookies.Delete(TokenAuthenticationSchemeOptions.CookieName);
            return RedirectToPage("/Login");
        }
    }
}