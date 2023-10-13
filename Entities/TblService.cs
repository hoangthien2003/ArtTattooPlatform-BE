using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblService
{
    public int ServiceID { get; set; }

    public string? ServiceName { get; set; }

    public string? Description { get; set; }

    public string? Price { get; set; }

    public string? CategoryID { get; set; }

    public string? ImageService { get; set; }

    public int? ArtistID { get; set; }

    public int? StudioID { get; set; }

    public virtual TblArtist? Artist { get; set; }

    public virtual TblStudio? Studio { get; set; }

    public virtual ICollection<TblBookingDetail> TblBookingDetails { get; set; } = new List<TblBookingDetail>();

    public virtual ICollection<TblBooking> TblBookings { get; set; } = new List<TblBooking>();

    public virtual ICollection<TblFeedback> TblFeedbacks { get; set; } = new List<TblFeedback>();

    public virtual ICollection<TblPayment> TblPayments { get; set; } = new List<TblPayment>();
}
