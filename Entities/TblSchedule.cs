using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblSchedule
{
    public int ScheduleID { get; set; }

    public DateTime? FreeTime { get; set; }

    public DateTime? EndTime { get; set; }

    public bool? Status { get; set; }

    public int? ArtistID { get; set; }

    public virtual TblArtist? Artist { get; set; }
}
