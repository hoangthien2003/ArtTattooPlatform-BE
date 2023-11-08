using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblBookingDetail
{
    public int BookingDetailId { get; set; }

    public int? BookingId { get; set; }

    public int? Quantity { get; set; }

    public int? ServiceId { get; set; }

    public int? ArtistId { get; set; }

    public decimal? Price { get; set; }

    public virtual TblArtist? Artist { get; set; }

    public virtual TblBooking? Booking { get; set; }

    public virtual TblService? Service { get; set; }
}
