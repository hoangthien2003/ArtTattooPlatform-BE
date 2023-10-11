using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblMember
{
    public int MemberID { get; set; }

    public string? MemberName { get; set; }

    public string? PhoneNumber { get; set; }

    public int? UserID { get; set; }

    public virtual ICollection<TblBooking> TblBookings { get; set; } = new List<TblBooking>();

    public virtual ICollection<TblFeedback> TblFeedbacks { get; set; } = new List<TblFeedback>();

    public virtual ICollection<TblPayment> TblPayments { get; set; } = new List<TblPayment>();

    public virtual TblUser? User { get; set; }
}
