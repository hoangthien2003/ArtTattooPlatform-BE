
﻿using back_end.Entities;

namespace back_end.Models
{
    public class Feedback
    {
        public int FeedbackID { get; set; }

        public string? FeedbackDetail { get; set; }

        public int? MemberID { get; set; }

        public int? ServiceID { get; set; }

        public DateTime? FeedbackDate { get; set; }

        public virtual ICollection<ImageFeedback> ImageFeedbacks { get; set; } = new List<ImageFeedback>();

        public virtual TblMember? Member { get; set; }

        public virtual TblService? Service { get; set; }
    }
}
