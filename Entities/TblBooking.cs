using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblBooking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public DateTime? BookingDate { get; set; }

    public string? PhoneNumber { get; set; }

    public decimal? Total { get; set; }

    public int? StudioId { get; set; }

    public int? ServiceId { get; set; }

    public string? Status { get; set; }

    public string? FullName { get; set; }

    public int? Quantity { get; set; }

    public virtual TblService? Service { get; set; }

    public virtual TblStudio? Studio { get; set; }

    public virtual TblUser? User { get; set; }
}
