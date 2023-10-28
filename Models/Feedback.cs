﻿namespace back_end.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }

        public string? FeedbackDetail { get; set; }

        public int? UserId { get; set; }

        public int? ServiceId { get; set; }

        public DateTime? FeedbackDate { get; set; }

    }
}
