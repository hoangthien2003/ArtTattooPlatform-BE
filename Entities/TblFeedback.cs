using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblFeedback
{
    public int FeedbackId { get; set; }

    public string? FeedbackDetail { get; set; }

    public int? MemberId { get; set; }

    public int? ServiceId { get; set; }

    public DateTime? FeedbackDate { get; set; }

    public int? Rating { get; set; }

    public virtual TblMember? Member { get; set; }

    public virtual TblService? Service { get; set; }
}
