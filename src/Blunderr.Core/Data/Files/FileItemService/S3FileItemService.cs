using Amazon.S3;
using Amazon.S3.Model;
using Blunderr.Core.Data.Entities.FileItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace Blunderr.Core.Data.Files.FileItemService
{
    public class S3FileItemService : IFileItemService
    {
        private readonly string LocationURL;
        private readonly string BucketName;
        private readonly IAmazonS3 _s3;

        public S3FileItemService(IConfiguration configuration, IAmazonS3 s3)
        {
            LocationURL = configuration.GetSection("AWS")["LocationURL"];
            BucketName = configuration.GetSection("AWS")["BucketName"];
            _s3 = s3;
        }

        public async Task HandleFileItemEntriesAsync(IQueryable<EntityEntry<FileItem>> entries)
        {
            FileItem[] addedFileItems = entries
                .Where(entry => entry.State == EntityState.Added)
                .Select(entry => entry.Entity)
                .ToArray();

            FileItem[] deletedFileItems = entries
                .Where(entry => entry.State == EntityState.Deleted)
                .Select(entry => entry.Entity)
                .ToArray();

            await Task.WhenAll(new Task[]
            {
                HandleAddedFileItemsAsync(addedFileItems),
                HandleDeletedFileItemsAsync(deletedFileItems)
            });
        }

        public string LocationOf(FileItem fileItem)
        {
            return LocationURL + fileItem.FileName;
        }

        private async Task HandleAddedFileItemsAsync(FileItem[] fileItems)
        {
            int numFileItems = fileItems.Count();

            Task[] tasks = new Task[numFileItems];

            for (int i = 0; i < numFileItems; i++)
            {
                FileItem fileItem = fileItems[i];
                fileItem.FileName = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz", System.Globalization.CultureInfo.InvariantCulture) + '.' + fileItem.DisplayName;

                PutObjectRequest request = new()
                {
                    BucketName = BucketName,
                    Key = fileItem.FileName,
                    InputStream = fileItem.FileStream
                };

                tasks[i] = _s3.PutObjectAsync(request);
            }

            await Task.WhenAll(tasks);
        }

        private async Task HandleDeletedFileItemsAsync(FileItem[] fileItems)
        {
            int numFileItems = fileItems.Count();

            Task[] tasks = new Task[numFileItems];

            for (int i = 0; i < numFileItems; i++)
            {
                DeleteObjectRequest request = new DeleteObjectRequest()
                {
                    BucketName = BucketName,
                    Key = fileItems[i].FileName
                };

                tasks[i] = _s3.DeleteObjectAsync(request);
            }

            await Task.WhenAll(tasks);
        }
    }
}