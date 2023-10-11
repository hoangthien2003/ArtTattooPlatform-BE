﻿using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblBooking
{
    public int BookingID { get; set; }

    public int? MemberID { get; set; }

    public int? StudioID { get; set; }

    public int? ServiceID { get; set; }

    public DateTime? BookingDate { get; set; }

    public string? PhoneNumber { get; set; }

    public decimal? Total { get; set; }

    public virtual TblMember? Member { get; set; }

    public virtual TblService? Service { get; set; }

    public virtual TblStudio? Studio { get; set; }

    public virtual ICollection<TblBookingDetail> TblBookingDetails { get; set; } = new List<TblBookingDetail>();
}
