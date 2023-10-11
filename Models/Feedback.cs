
﻿using back_end.Entities;

namespace back_end.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }

        public string? FeedbackDetail { get; set; }

        public int? MemberId { get; set; }

        public int? ServiceId { get; set; }

        public DateTime? FeedbackDate { get; set; }

        public virtual ICollection<ImageFeedback> ImageFeedbacks { get; set; } = new List<ImageFeedback>();

        public virtual TblMember? Member { get; set; }

        public virtual TblService? Service { get; set; }
    }
}
