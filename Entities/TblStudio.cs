using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblStudio
{
    public int StudioId { get; set; }

    public string? StudioName { get; set; }

    public int? IsActive { get; set; }

    public string? Address { get; set; }

    public string? StudioPhone { get; set; }

    public string? StudioEmail { get; set; }

    public int? ManagerId { get; set; }

    public virtual TblManager? Manager { get; set; }

    public virtual ICollection<TblService> TblServices { get; set; } = new List<TblService>();
}
