using System.Diagnostics.CodeAnalysis;

namespace huynhkimthang_0145_Final_LTC_.Models
{
    public class AnouncementViewModel
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        [AllowNull]
        public IFormFile ThumbnailImg { get; set; }
        public int? AnnCateId { get; set; }

    }
}
