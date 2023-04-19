namespace Blunderr.Core.Features.Tickets.TicketEdit.SaveTicketEdit
{
    public class SaveTicketEditResponse
    {
        public SaveError? Error { get; set; }
    }

    public enum SaveError
    {
        Forbidden,
        NotFound,
        RemovedAttachmentNotFound
    }
}