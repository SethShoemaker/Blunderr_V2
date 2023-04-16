using Blunderr.Core.Features.Tickets.TicketCreate.Query;
using Blunderr.Core.Features.Tickets.TicketCreate.SaveTicket;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blunderr.Web.Pages.Tickets
{
    [Authorize]
    public class Create : PageModel
    {
        private readonly IMediator _mediator;

        public Create(IMediator mediator)
        {
            _mediator = mediator;
        }

        public GetCreateDataResponse CreateData { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync()
        {
            CreateData = await _mediator.Send(new GetCreateDataRequest(User.Role(), User.Id()));
            return Page();
        }

        [BindProperty]
        public IFormFileCollection Files { get; set; } = null!;

        [BindProperty]
        public SaveTicketRequest SaveRequest { get; set; } = new();

        public SaveTicketResponse SaveResponse { get; set; } = null!;

        public async Task<IActionResult> OnPostAsync()
        {
            SaveRequest.CreatorRole = User.Role();
            SaveRequest.CreatorId = User.Id();

            SaveRequest.Attachments = Files
                .Select(f => new AttachmentDto(f.OpenReadStream(), f.FileName))
                .ToList();

            SaveResponse = await _mediator.Send(SaveRequest);

            if(SaveResponse.Errors.Contains(SaveError.Forbidden)) Forbid();
            if(SaveResponse.Errors.Any()) return BadRequest();

            return RedirectToPage("/Tickets/Show", new { TicketId = SaveResponse.TicketId });
        }
    }
}