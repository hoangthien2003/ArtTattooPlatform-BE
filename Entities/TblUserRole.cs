using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblUserRole
{
    public int UserRoleId { get; set; }

    public int? UserId { get; set; }

    public string? RoleId { get; set; }
}
