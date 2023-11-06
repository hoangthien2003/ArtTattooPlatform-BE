using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblCategory
{
    public string CategoryId { get; set; } = null!;

    public string? CategoryName { get; set; }

    public virtual ICollection<TblService> TblServices { get; set; } = new List<TblService>();
}
