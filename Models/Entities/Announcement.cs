using System;
using System.Collections.Generic;

namespace huynhkimthang_0145_Final_LTC_.Models.Entities;

public partial class Announcement
{
    public int AnnId { get; set; }

    public int? PostId { get; set; }

    public int? AnnCateId { get; set; }

    public virtual AnnouncementCategory? AnnCate { get; set; }

    public virtual Post? Post { get; set; }
}
