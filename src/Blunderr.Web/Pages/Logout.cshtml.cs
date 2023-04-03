using Blunderr.Web.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blunderr.Web.Pages
{
    public class Logout : PageModel
    {
        public IActionResult OnGet()
        {
            Response.Cookies.Delete(TokenAuthenticationSchemeOptions.CookieName);
            return RedirectToPage("/Login");
        }
    }
}