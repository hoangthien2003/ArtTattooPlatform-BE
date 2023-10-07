using System;
using System.Collections.Generic;

namespace back_end.Model;

public partial class TblService
{
    public int ServiceId { get; set; }

    public string? ServiceName { get; set; }

    public string? Image { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public string? CategoryId { get; set; }

    public string? ImageService { get; set; }

    public bool? IsActive { get; set; }

    public int? ServiceItemId { get; set; }

    public int? ArtistId { get; set; }

    public virtual TblArtist? Artist { get; set; }

    public virtual ICollection<TblBookingDetail> TblBookingDetails { get; set; } = new List<TblBookingDetail>();

    public virtual ICollection<TblFeedback> TblFeedbacks { get; set; } = new List<TblFeedback>();

    public virtual ICollection<TblStudio> TblStudios { get; set; } = new List<TblStudio>();
}
