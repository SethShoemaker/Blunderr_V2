using Blunderr.Core.Features.Projects.ProjectDelete;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blunderr.Web.Pages.Projects
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
        public int ProjectId { get; set; }

        public ProjectDeleteResponse Data { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync()
        {
            Data = await _mediator.Send(new ProjectDeleteRequest(User.Role(), User.Id(), ProjectId));

            if(Data.Error == Error.NotFound) return NotFound();
            else if(Data.Error == Error.Forbidden) return Forbid();
            else return RedirectToPage("/Projects/Index");
        }
    }
}