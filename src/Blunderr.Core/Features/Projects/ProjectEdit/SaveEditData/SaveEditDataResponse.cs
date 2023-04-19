namespace Blunderr.Core.Features.Projects.ProjectEdit.SaveEditData
{
    public class SaveEditDataResponse
    {
        public SaveError? Error { get; set; }
    }

    public enum SaveError
    {
        Forbidden,
        NotFound
    }
}