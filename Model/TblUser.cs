using System;
using System.Collections.Generic;

namespace back_end.Model;

public partial class TblUser
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool? Status { get; set; }

    public DateTime? CreateUser { get; set; }

    public string? Image { get; set; }

    public string? RoleId { get; set; }

    public virtual TblRole? Role { get; set; }

    public virtual ICollection<TblManager> TblManagers { get; set; } = new List<TblManager>();

    public virtual ICollection<TblMember> TblMembers { get; set; } = new List<TblMember>();
}
