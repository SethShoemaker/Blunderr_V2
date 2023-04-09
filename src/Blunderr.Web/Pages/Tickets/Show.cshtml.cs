using Blunderr.Core.Features.Tickets.TicketCommentCreate;
using Blunderr.Core.Features.Tickets.TicketShow;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blunderr.Web.Pages.Tickets
{
    public class Show : PageModel
    {
        private readonly IMediator _mediator;

        public Show(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
        }

        [FromRoute]
        public int TicketId { get; set; }

        public TicketShowResponse GetData { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync()
        {
            GetData = await _mediator.Send(new TicketShowRequest(User.Role(), User.Id(), TicketId));

            return GetData.TicketExists() ? Page() : NotFound();
        }

        [BindProperty]
        public string CommentContent { get; set; } = null!;

        [BindProperty]
        public IFormFileCollection CommentAttachments { get; set; } = null!;

        public TicketCommentCreateResponse PostData { get; set; } = null!;

        public async Task<IActionResult> OnPostAsync()
        {
            PostData = await _mediator.Send(new TicketCommentCreateRequest(
                User.Role(),
                User.Id(),
                TicketId,
                CommentContent,
                CommentAttachments.Select(file => new FileItemDto(file.FileName, file.OpenReadStream()))
            ));

            return PostData.isSuccessful() ? RedirectToPage("/Tickets/Show", TicketId) : BadRequest();
        }
    }
}