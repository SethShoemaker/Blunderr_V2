namespace Blunderr.Core.Features.Projects.ProjectCreate.SaveCreateData
{
    public class SaveCreateDataResponse
    {
        public int ProjectId { get; set; }

        public SaveError? Error { get; set; }
    }

    public enum SaveError
    {
        Forbidden,
        ClientNotFound
    }
}