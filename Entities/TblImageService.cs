using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblImageService
{
    public int ImageId { get; set; }

    public int? ServiceId { get; set; }

    public string? ImagePath { get; set; }

    public virtual TblService? Service { get; set; }
}
