namespace Blunderr.Core.Features.Tickets.TicketCreate.SaveTicket
{
    public class SaveTicketResponse
    {
        public List<Error> Errors { get; } = new();

        public int TicketId { get; set; }

        public bool hasErrors() => Errors.Any();
    }

    public enum Error
    {
        Forbidden,
        ProjectNotFound,
        TitleNull,
        DescriptionNull
    }
}