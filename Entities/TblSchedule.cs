using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblSchedule
{
    public int ScheduleId { get; set; }

    public DateTime? FreeTime { get; set; }

    public DateTime? EndTime { get; set; }

    public bool? Status { get; set; }

    public int? ArtistId { get; set; }

    public virtual TblArtist? Artist { get; set; }
}
