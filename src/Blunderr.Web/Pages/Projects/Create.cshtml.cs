using Blunderr.Core.Features.Projects.ProjectCreate.GetCreateData;
using Blunderr.Core.Features.Projects.ProjectCreate.SaveCreateData;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blunderr.Web.Pages.Projects
{
    [Authorize]
    public class Create : PageModel
    {
        private readonly IMediator _mediator;

        public Create(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<ClientDto> Clients { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync()
        {            
            GetCreateDataResponse createData = await _mediator.Send(new GetCreateDataRequest(User.Role(), User.Id()));

            if(createData.Error == Error.Forbidden) return Forbid();

            Clients = createData.Clients;

            return Page();
        }

        [BindProperty]
        public SaveCreateDataRequest SaveRequest { get; set; } = null!;

        public async Task<IActionResult> OnPostAsync()
        {
            SaveRequest.CreatorRole = User.Role();
            SaveRequest.CreatorId = User.Id();

            SaveCreateDataResponse saveResponse = await _mediator.Send(SaveRequest);

            if(saveResponse.Error == SaveError.Forbidden) return Forbid();
            if(saveResponse.Error == SaveError.ClientNotFound) return BadRequest();

            return RedirectToPage("/Projects/Show", new { ProjectId = saveResponse.ProjectId });
        }
    }
}