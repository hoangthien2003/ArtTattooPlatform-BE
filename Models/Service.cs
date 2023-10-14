﻿namespace back_end.Models
{
    public class Service
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string CategoryID { get; set; }
        public string Price { get; set; }
        public IFormFile Image {  get; set; }

        public int Rating { get; set; }
    }
}
