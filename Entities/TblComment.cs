using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblComment
{
    public int CommentId { get; set; }

    public int? FeedbackId { get; set; }

    public string? CommentText { get; set; }

    public virtual TblFeedback? Feedback { get; set; }
}
