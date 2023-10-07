using System;
using System.Collections.Generic;

namespace back_end.Model;

public partial class TblBooking
{
    public int BookingId { get; set; }

    public int? MemberId { get; set; }

    public DateTime? BookingDate { get; set; }

    public int? StudioId { get; set; }

    public virtual TblMember? Member { get; set; }

    public virtual ICollection<TblBookingDetail> TblBookingDetails { get; set; } = new List<TblBookingDetail>();
}
