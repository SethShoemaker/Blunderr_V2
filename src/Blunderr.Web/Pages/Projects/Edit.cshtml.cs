using Blunderr.Core.Features.Projects.ProjectEdit.GetEditData;
using Blunderr.Core.Features.Projects.ProjectEdit.SaveEditData;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blunderr.Web.Pages.Projects
{
    [Authorize]
    public class Edit : PageModel
    {
        private readonly IMediator _mediator;

        public Edit(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FromRoute]
        public int ProjectId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            GetEditDataResponse editData = await _mediator.Send(new GetEditDataRequest(User.Role(), User.Id(), ProjectId));

            if(editData.Error == Error.Forbidden) return Forbid();
            if(editData.Error == Error.NotFound) return NotFound();

            SaveRequest = editData.SaveRequest;

            return Page();
        }

        [BindProperty]
        public SaveEditDataRequest SaveRequest { get; set; } = null!;

        public async Task<IActionResult> OnPostAsync()
        {
            SaveRequest.EditorRole = User.Role();
            SaveRequest.EditorId = User.Id();
            SaveRequest.ProjectId = ProjectId;

            SaveEditDataResponse saveResponse = await _mediator.Send(SaveRequest);

            if(saveResponse.Error == SaveError.Forbidden) return Forbid();
            if(saveResponse.Error == SaveError.NotFound) return NotFound();

            return RedirectToPage("/Projects/Show", new { ProjectId = ProjectId });
        }
    }
}