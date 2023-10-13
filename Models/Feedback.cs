
﻿using back_end.Entities;

namespace back_end.Models
{
    public class Feedback
    {
        public int FeedbackID { get; set; }

        public string? FeedbackDetail { get; set; }

        public int? MemberID { get; set; }

        public int? ServiceID { get; set; }

        public int? Rating { get; set; }

        public DateTime? FeedbackDate { get; set; }

        

    }
}
