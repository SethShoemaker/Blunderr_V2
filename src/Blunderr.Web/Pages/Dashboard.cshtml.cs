using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blunderr.Web.Pages
{
    [Authorize]
    public class Dashboard : PageModel
    {
        private readonly ILogger<Dashboard> _logger;

        public Dashboard(ILogger<Dashboard> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}