using Blunderr.Core.Features.Tickets.TicketDelete;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blunderr.Web.Pages.Tickets
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
        public int TicketId { get; set; }

        public TicketDeleteResponse Data { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync()
        {
            Data = await _mediator.Send(new TicketDeleteRequest(User.Role(), User.Id(), TicketId));

            if(Data.Error == Error.Forbidden) return Forbid();
            if(Data.Error == Error.NotFound) return NotFound();

            return RedirectToPage("/Tickets/Index");
        }
    }
}