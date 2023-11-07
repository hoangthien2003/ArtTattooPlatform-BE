using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblMember
{
    public int MemberId { get; set; }

    public string? MemberName { get; set; }

    public string? PhoneNumber { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<TblPayment> TblPayments { get; set; } = new List<TblPayment>();

    public virtual TblUser? User { get; set; }
}
