using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblBooking
{
    public string BookingId { get; set; } = null!;

    public int? UserId { get; set; }

    public int? StudioId { get; set; }

    public int? ServiceId { get; set; }

    public DateTime? BookingDate { get; set; }

    public string? PhoneNumber { get; set; }

    public decimal? Total { get; set; }

    public string? Status { get; set; }

    public virtual TblService? Service { get; set; }

    public virtual TblStudio? Studio { get; set; }

    public virtual ICollection<TblBookingDetail> TblBookingDetails { get; set; } = new List<TblBookingDetail>();

    public virtual TblMember? User { get; set; }

    public virtual TblUser? UserNavigation { get; set; }
}
