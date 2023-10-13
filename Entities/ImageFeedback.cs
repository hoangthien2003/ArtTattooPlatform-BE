using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class ImageFeedback
{
    public int ImageFeedbackID { get; set; }

    public string? Image { get; set; }

    public int? FeedbackId { get; set; }

    public virtual TblFeedback? Feedback { get; set; }
}
