using System;
using System.Collections.Generic;

namespace back_end.Model;

public partial class TblMember
{
    public int MemberId { get; set; }

    public string? MemberName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? CreateMember { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<TblArtist> TblArtists { get; set; } = new List<TblArtist>();

    public virtual ICollection<TblBooking> TblBookings { get; set; } = new List<TblBooking>();

    public virtual ICollection<TblFeedback> TblFeedbacks { get; set; } = new List<TblFeedback>();

    public virtual TblUser? User { get; set; }
}
