namespace Blunderr.Core.Features.Tickets.TicketDelete
{
    public class TicketDeleteResponse
    {
        public Error? Error { get; set; }

        public bool isSuccessful() => Error == null;
    }

    public enum Error
    {
        NotFound,
        Forbidden
    }
}