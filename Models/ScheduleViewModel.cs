﻿namespace huynhkimthang_0145_Final_LTC_.Models
{
    public class ScheduleViewModel
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public IFormFile ThumbnailImg { get; set; }
        public DateTime? Time { get; set; }
        public string? Location { get; set; }
    }
}
