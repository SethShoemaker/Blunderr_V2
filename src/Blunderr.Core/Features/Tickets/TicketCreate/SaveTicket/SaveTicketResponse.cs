namespace Blunderr.Core.Features.Tickets.TicketCreate.SaveTicket
{
    public class SaveTicketResponse
    {
        public List<SaveError> Errors { get; } = new();

        public int TicketId { get; set; }

        public bool hasErrors() => Errors.Any();
    }

    public enum SaveError
    {
        Forbidden,
        ProjectNotFound,
        TitleNull,
        DescriptionNull
    }
}