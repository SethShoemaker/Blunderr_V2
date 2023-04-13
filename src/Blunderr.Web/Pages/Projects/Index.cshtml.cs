using Blunderr.Core.Features.Projects.ProjectList;
using Blunderr.Core.Services.PaginationService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blunderr.Web.Pages.Projects
{
    [Authorize]
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
        public int? ClientId { get; set; }

        public ProjectListResponse Data { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync()
        {
            Data = await _mediator.Send(new ProjectListRequest(
                User.Role(),
                User.Id(),
                PageNumber,
                PageSize,
                ClientId
            ));

            if(!Data.Page.isSuccessFull() && Data.Page.Error == Error.PageNumberOutOfRange)
                RedirectToPage("/Projects/Index");

            return Page();
        }
    }
}