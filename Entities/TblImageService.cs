using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblImageService
{
    public int ImageServiceId { get; set; }

    public string? Image { get; set; }

    public int? ServiceId { get; set; }

    public virtual TblService? Service { get; set; }
}
