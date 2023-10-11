using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblManager
{
    public int ManagerID { get; set; }

    public string? ManagerName { get; set; }

    public string? Gender { get; set; }

    public string? ManagerPhone { get; set; }

    public int? UserID { get; set; }

    public virtual ICollection<TblStudio> TblStudios { get; set; } = new List<TblStudio>();

    public virtual TblUser? User { get; set; }
}
