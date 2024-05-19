using System;
using System.Collections.Generic;

namespace huynhkimthang_0145_Final_LTC_.Models.Entities;

public partial class Schedule
{
    public int SchId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Loacation { get; set; }

    public int? PostId { get; set; }

    public virtual Post? Post { get; set; }
}
