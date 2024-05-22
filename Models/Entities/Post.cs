using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace huynhkimthang_0145_Final_LTC_.Models.Entities;

public partial class Post
{
    public int PostId { get; set; }
    [StringLength(150, MinimumLength = 20, ErrorMessage = "Title length must be between 20 and 150 characters.")]
    public string? Title { get; set; }

    public string? Content { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateOnly? CreatedDate { get; set; }

    public string? ThumbnailImg { get; set; }

    public string? Img1 { get; set; }

    public string? Img2 { get; set; }

    public string? Img3 { get; set; }

    public int? PostType { get; set; }

    public int? EmpId { get; set; }

    public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();

    public virtual Employee? Emp { get; set; }

    public virtual PostCategory? PostTypeNavigation { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
