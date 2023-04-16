using Blunderr.Core.Features.Projects.ProjectShow;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blunderr.Web.Pages.Projects
{
    [Authorize]
    public class Show : PageModel
    {
        private readonly IMediator _mediator;

        public Show(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FromRoute]
        public int ProjectId { get; set; }

        public ProjectShowResponse Data { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync()
        {
            Data = await _mediator.Send(new ProjectShowRequest(User.Role(), User.Id(), ProjectId));

            if(Data.Error == Error.NotFound) return NotFound();
            if(Data.Error == Error.Forbidden) return Forbid();
            return Page();
        }
    }
}