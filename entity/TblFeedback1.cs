using System;
using System.Collections.Generic;

namespace back_end.entity;

public partial class TblFeedback1
{
    public int FeedbackId { get; set; }

    public string? FeedbackDetail { get; set; }

    public int? MemberId { get; set; }

    public int? ServiceId { get; set; }

    public DateTime? FeedbackDate { get; set; }
}
