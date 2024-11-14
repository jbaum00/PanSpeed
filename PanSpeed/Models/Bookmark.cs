﻿namespace PanSpeed.Models
{
    public class Bookmark
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Category { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
