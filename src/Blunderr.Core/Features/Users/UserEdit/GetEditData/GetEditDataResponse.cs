using Blunderr.Core.Features.Users.UserEdit.SaveEditData;

namespace Blunderr.Core.Features.Users.UserEdit.GetEditData
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