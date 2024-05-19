using System;
using System.Collections.Generic;

namespace huynhkimthang_0145_Final_LTC_.Models.Entities;

public partial class AnnouncementCategory
{
    public int AnnCateId { get; set; }

    public string? AnnCateName { get; set; }

    public string? AnnCateDesc { get; set; }

    public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
}
