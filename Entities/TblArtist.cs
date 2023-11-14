﻿using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblArtist
{
    public int ArtistId { get; set; }

    public string? ArtistName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Biography { get; set; }

    public int? UserId { get; set; }

    public string? Certificate { get; set; }

    public string? AvatarArtist { get; set; }

    public virtual ICollection<TblSchedule> TblSchedules { get; set; } = new List<TblSchedule>();

    public virtual TblUser? User { get; set; }
}
