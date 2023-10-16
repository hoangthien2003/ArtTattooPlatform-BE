using System;
using System.Collections.Generic;

namespace back_end.entity;

public partial class TblPayment
{
    public int PaymentId { get; set; }

    public int? ServiceId { get; set; }

    public int? MemberId { get; set; }

    public decimal? PaymentAmount { get; set; }

    public decimal? Bonus { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? PaymentStatus { get; set; }

    public string? Currency { get; set; }

    public virtual TblMember? Member { get; set; }

    public virtual TblService? Service { get; set; }
}
