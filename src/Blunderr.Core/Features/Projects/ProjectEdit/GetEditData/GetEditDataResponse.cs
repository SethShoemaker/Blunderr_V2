using Blunderr.Core.Features.Projects.ProjectEdit.SaveEditData;

namespace Blunderr.Core.Features.Projects.ProjectEdit.GetEditData
{
    public class GetEditDataResponse
    {
        public SaveEditDataRequest SaveRequest { get; set; } = null!;

        public Error? Error { get; set; }
    }

    public enum Error
    {
        Forbidden,
        NotFound
    }
}