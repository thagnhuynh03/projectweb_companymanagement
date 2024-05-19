using System;
using System.Collections.Generic;

namespace huynhkimthang_0145_Final_LTC_.Models.Entities;

public partial class PostCategory
{
    public int PostCateId { get; set; }

    public string? PostCateName { get; set; }

    public string? PostCateDesc { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
