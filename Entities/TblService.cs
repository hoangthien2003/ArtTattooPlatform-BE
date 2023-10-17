using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblService
{
    public int ServiceId { get; set; }

    public string? ServiceName { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public string? CategoryId { get; set; }

    public string? ImageService { get; set; }

    public int? ArtistId { get; set; }

    public int? StudioId { get; set; }

    public int? Rating { get; set; }

    public virtual TblArtist? Artist { get; set; }

    public virtual TblStudio? Studio { get; set; }

    public virtual ICollection<TblBookingDetail> TblBookingDetails { get; set; } = new List<TblBookingDetail>();

    public virtual ICollection<TblBooking> TblBookings { get; set; } = new List<TblBooking>();

    public virtual ICollection<TblFeedback> TblFeedbacks { get; set; } = new List<TblFeedback>();

    public virtual ICollection<TblPayment> TblPayments { get; set; } = new List<TblPayment>();
}
