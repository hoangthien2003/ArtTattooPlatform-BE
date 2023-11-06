﻿using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblStudio
{
    public int StudioId { get; set; }

    public string? StudioName { get; set; }

    public string? Address { get; set; }

    public string? StudioPhone { get; set; }

    public string? StudioEmail { get; set; }

    public string? Description { get; set; }

    public string? Logo { get; set; }

    public string? OpenTime { get; set; }

    public string? EndTime { get; set; }

    public double? RatingStb { get; set; }

    public virtual ICollection<TblBooking> TblBookings { get; set; } = new List<TblBooking>();

    public virtual ICollection<TblManager> TblManagers { get; set; } = new List<TblManager>();

    public virtual ICollection<TblService> TblServices { get; set; } = new List<TblService>();
}
