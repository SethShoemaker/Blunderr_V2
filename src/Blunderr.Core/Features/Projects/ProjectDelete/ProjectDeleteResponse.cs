namespace Blunderr.Core.Features.Projects.ProjectDelete
{
    public class ProjectDeleteResponse
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