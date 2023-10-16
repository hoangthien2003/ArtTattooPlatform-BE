using System;
using System.Collections.Generic;

namespace back_end.entity;

public partial class TblManager
{
    public int ManagerId { get; set; }

    public string? ManagerName { get; set; }

    public string? Gender { get; set; }

    public string? ManagerPhone { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<TblStudio> TblStudios { get; set; } = new List<TblStudio>();

    public virtual TblUser? User { get; set; }
}
