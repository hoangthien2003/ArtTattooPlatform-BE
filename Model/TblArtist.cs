using System;
using System.Collections.Generic;

namespace back_end.Model;

public partial class TblArtist
{
    public int ArtistId { get; set; }

    public string? FisrtName { get; set; }

    public string? LastName { get; set; }

    public string? FullName { get; set; }

    public bool? Gender { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? CreateArtist { get; set; }

    public string? ImageArtist { get; set; }

    public string? Biography { get; set; }

    public int? MemberId { get; set; }

    public int? UserId { get; set; }

    public virtual TblMember? Member { get; set; }

    public virtual ICollection<TblBookingDetail> TblBookingDetails { get; set; } = new List<TblBookingDetail>();

    public virtual ICollection<TblCetificate> TblCetificates { get; set; } = new List<TblCetificate>();


    public virtual ICollection<TblSchedule> TblSchedules { get; set; } = new List<TblSchedule>();

    public virtual ICollection<TblService> TblServices { get; set; } = new List<TblService>();
}
