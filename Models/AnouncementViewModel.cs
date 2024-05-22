using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace huynhkimthang_0145_Final_LTC_.Models
{
    public class AnouncementViewModel
    {
        [StringLength(150, MinimumLength = 20, ErrorMessage = "Title length must be between 20 and 150 characters.")]
        public string? Title { get; set; }
        public string? Content { get; set; }
        [AllowNull]
        public IFormFile ThumbnailImg { get; set; }
        public int? AnnCateId { get; set; }

    }
}
