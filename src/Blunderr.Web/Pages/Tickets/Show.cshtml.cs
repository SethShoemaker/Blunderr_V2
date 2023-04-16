using Blunderr.Core.Features.Tickets.TicketCommentCreate;
using Blunderr.Core.Features.Tickets.TicketShow;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blunderr.Web.Pages.Tickets
{
    [Authorize]
    public class Show : PageModel
    {
        private readonly IMediator _mediator;

        public Show(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
        }

        [FromRoute]
        public int TicketId { get; set; }

        public TicketShowResponse Data { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync()
        {
            Data = await _mediator.Send(new TicketShowRequest(User.Role(), User.Id(), TicketId));

            if(Data.Error == Error.Forbidden) return Forbid();
            if(Data.Error == Error.NotFound) return NotFound();

            return Page();
        }

        [BindProperty]
        public string CommentContent { get; set; } = null!;

        [BindProperty]
        public IFormFileCollection CommentAttachments { get; set; } = null!;

        public TicketCommentCreateResponse SaveResponse { get; set; } = null!;

        public async Task<IActionResult> OnPostAsync()
        {
            SaveResponse = await _mediator.Send(new TicketCommentCreateRequest(
                User.Role(),
                User.Id(),
                TicketId,
                CommentContent,
                CommentAttachments.Select(file => new FileItemDto(file.FileName, file.OpenReadStream()))
            ));

            if(SaveResponse.Error == SaveError.Forbidden) return Forbid();
            if(SaveResponse.Error == SaveError.TicketNotFound) return BadRequest();

            return RedirectToPage("/Tickets/Show", TicketId);
        }
    }
}