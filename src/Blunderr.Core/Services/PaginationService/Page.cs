namespace Blunderr.Core.Services.PaginationService
{
    public class Page<T>
    {
        public List<T> Items { get; set; } = null!;

        public int StartPosition { get; set; }

        public int EndPosition { get; set; }

        public int TotalCount { get; set; }

        public bool HasPrevPage { get; set; }

        public bool HasNextPage { get; set; }

        public int MaxPageNumber { get; set; }

        public PageError? Error { get; set; }

        public bool isSuccessFull() => Error is null;
    }

    public enum PageError
    {
        PageNumberOutOfRange,
    }
}