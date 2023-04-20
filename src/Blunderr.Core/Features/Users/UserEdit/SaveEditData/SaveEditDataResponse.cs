namespace Blunderr.Core.Features.Users.UserEdit.SaveEditData
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