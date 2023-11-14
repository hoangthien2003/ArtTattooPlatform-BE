﻿namespace back_end.Models
{
    public class Studio
    {
        public int? StudioID { get; set; }
        public string StudioName { get; set; }
        public string Address { get; set; }
        public string StudioPhone { get; set; }
        public string StudioEmail {  get; set; }
        public int? ManagerID { get; set; }
        public string Description {  get; set; }
        public string? Logo { get; set; }
    }
}
