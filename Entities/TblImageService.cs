using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblImageService
{
    public int ImageServiceID { get; set; }

    public string? Image { get; set; }

    public int? ServiceID { get; set; }

    public virtual TblService? Service { get; set; }
}
