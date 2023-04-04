using Blunderr.Core.Data.Entities.Tickets;
using Blunderr.Core.Features.Tickets.TicketList;
using Blunderr.Core.Services.PaginationService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blunderr.Web.Pages.Tickets
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
        public int PageSize { get; set; } = 10;

        [FromQuery]
        public int PageNumber { get; set; } = 1;

        [FromQuery]
        public TicketStatus? Status { get; set; }

        [FromQuery]
        public int? SubmitterId { get; set; }

        [FromQuery]
        public int? ProjectId { get; set; }

        [FromQuery]
        public int? DeveloperId { get; set; }

        public TicketListResponse Data { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync()
        {
            Data = await _mediator.Send(new TicketListRequest(
                User.Role(),
                User.Id(),
                PageNumber,
                PageSize,
                SubmitterId,
                ProjectId,
                DeveloperId,
                Status
            ));

            if(!Data.Page.isSuccessFull() && Data.Page.Error == Error.PageNumberOutOfRange)
                RedirectToPage("/Tickets/Index");

            return Page();
        }
    }
}