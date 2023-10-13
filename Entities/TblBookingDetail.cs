using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblBookingDetail
{
    public int BookingDetailID { get; set; }

    public int? BookingID { get; set; }

    public int? Quantity { get; set; }

    public int? ServiceID { get; set; }

    public int? ArtistID { get; set; }

    public decimal? Price { get; set; }

    public virtual TblArtist? Artist { get; set; }

    public virtual TblBooking? Booking { get; set; }

    public virtual TblService? Service { get; set; }
}
