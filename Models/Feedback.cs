
﻿using back_end.Entities;

namespace back_end.Models
{
    public class Feedback
    {
        


        public string? FeedbackDetail { get; set; }

        public int? UserID { get; set; }

        public int? ServiceID { get; set; }

        public int? Rating { get; set; }

        
    }
}
