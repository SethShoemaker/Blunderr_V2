using Blunderr.Core.Data.Entities.FileItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace Blunderr.Core.Data.Files.FileItemService
{
    public class S3FileItemService : IFileItemService
    {
        private string LocationURL { get; set; }

        public S3FileItemService(IConfiguration configuration)
        {
            LocationURL = configuration.GetSection("FileService")["LocationURL"];
        }

        public async Task<bool> HandleFileItemEntriesAsync(IQueryable<EntityEntry<FileItem>> entries)
        {
            IEnumerable<FileItem> addedFileItems = await entries
                .Where(entry => entry.State == EntityState.Added)
                .Select(entry => entry.Entity)
                .ToArrayAsync();

            IEnumerable<FileItem> deletedFileItems = await entries
                .Where(entry => entry.State == EntityState.Deleted)
                .Select(entry => entry.Entity)
                .ToArrayAsync();

            return true;
        }

        public string LocationOf(FileItem fileItem)
        {
            return LocationURL + fileItem.FilePath;
        }
    }
}