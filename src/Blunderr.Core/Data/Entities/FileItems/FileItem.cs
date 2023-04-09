using System.ComponentModel.DataAnnotations.Schema;

namespace Blunderr.Core.Data.Entities.FileItems
{
    public class FileItem : Entity
    {
        [NotMapped]
        public Stream FileStream { get; set; } = null!;

        public string FileName { get; set; } = null!;

        public string DisplayName { get; set; } = null!;
    }
}