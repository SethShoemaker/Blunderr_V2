using Blunderr.Core.Features.Tickets.TicketEdit.SaveTicketEdit;

namespace Blunderr.Core.Features.Tickets.TicketEdit.GetEditData
{
    public class GetEditDataResponse
    {
        public SaveTicketEditRequest SaveRequest { get; set; } = null!;

        public List<DeveloperDto> Developers { get; set; } = null!;

        public Error? Error { get; set; }

        public bool IsSuccessful() => Error is null;
    }

    public class DeveloperDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }

    public enum Error
    {
        Forbidden,
        NotFound
    }
}