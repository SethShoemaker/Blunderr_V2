using Blunderr.Core.Data.Entities.FileItems;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Blunderr.Core.Data.Files.FileItemService
{
    public interface IFileItemService
    {
        public Task<bool> HandleFileItemEntriesAsync(IQueryable<EntityEntry<FileItem>> entries);

        string LocationOf(FileItem fileItem);
    }
}