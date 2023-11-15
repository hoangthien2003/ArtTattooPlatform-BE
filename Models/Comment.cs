namespace back_end.Models
{
    public class Comment
    {
        public int CommentID { get; set; }

        public int? FeedbackID { get; set; }

        public string? CommentDetail { get; set; }
    }
}
