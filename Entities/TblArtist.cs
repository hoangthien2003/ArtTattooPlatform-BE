using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblArtist
{
    public int ArtistID { get; set; }

    public string? ArtistName { get; set; }

    public bool? Gender { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Biography { get; set; }

    public int? UserID { get; set; }

    public string? Certificate { get; set; }

    public string? AvatarArtist { get; set; }

    public virtual ICollection<TblBookingDetail> TblBookingDetails { get; set; } = new List<TblBookingDetail>();

    public virtual ICollection<TblSchedule> TblSchedules { get; set; } = new List<TblSchedule>();

    public virtual ICollection<TblService> TblServices { get; set; } = new List<TblService>();
}
