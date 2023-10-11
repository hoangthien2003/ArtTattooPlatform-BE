using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblFeedback
{
    public int FeedbackID { get; set; }

    public string? FeedbackDetail { get; set; }

    public int? MemberID { get; set; }

    public int? ServiceID { get; set; }

    public DateTime? FeedbackDate { get; set; }

    public virtual TblMember? Member { get; set; }

    public virtual TblService? Service { get; set; }
}
