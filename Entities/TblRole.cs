using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblRole
{
    public string RoleID { get; set; } = null!;

    public string? RoleName { get; set; }

    public virtual ICollection<TblUser> TblUsers { get; set; } = new List<TblUser>();
}
