using Blunderr.Core.Features.Tickets.TicketEdit.GetEditData;
using Blunderr.Core.Features.Tickets.TicketEdit.SaveTicketEdit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blunderr.Web.Pages.Tickets
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
        public int TicketId { get; set; }

        public GetEditDataResponse EditDataResponse { get; set; } = null!;

        [BindProperty]
        public SaveTicketEditRequest SaveRequest { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync()
        {
            EditDataResponse = await _mediator.Send(new GetEditDataRequest(User.Role(), User.Id(), TicketId));

            if(EditDataResponse.Error == Error.Forbidden) return Forbid();
            if(EditDataResponse.Error == Error.NotFound) return NotFound();

            SaveRequest = EditDataResponse.SaveRequest;

            return Page();
        }

        [BindProperty]
        public IFormFileCollection Files { get; set; } = null!;

        public SaveTicketEditResponse SaveResponse { get; set; } = null!;

        public async Task<IActionResult> OnPostAsync()
        {
            SaveRequest.EditorRole = User.Role();
            SaveRequest.EditorId = User.Id();
            SaveRequest.TicketId = TicketId;

            SaveResponse = await _mediator.Send(SaveRequest);

            if(SaveResponse.Error == SaveError.Forbidden) return Forbid();
            if(SaveResponse.Error == SaveError.NotFound) return NotFound();
            if(SaveResponse.Error == SaveError.RemovedAttachmentNotFound) return BadRequest();

            return RedirectToPage("/Tickets/Show", new { TicketId = TicketId });
        }
    }
}