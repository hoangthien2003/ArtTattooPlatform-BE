namespace back_end.Models
{
    public class FeedbackRequest
    {
        public int FeedbackId { get; set; }

        public string? FeedbackDetail { get; set; }

        public int? MemberId { get; set; }

        public int? ServiceId { get; set; }

        public DateTime? FeedbackDate { get; set; }

    }
}
