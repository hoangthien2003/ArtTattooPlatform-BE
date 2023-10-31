using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblUser
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime? CreateUser { get; set; }

    public string? Image { get; set; }

    public string? RoleId { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual TblRole? Role { get; set; }

    public virtual ICollection<TblFeedback> TblFeedbacks { get; set; } = new List<TblFeedback>();

    public virtual ICollection<TblManager> TblManagers { get; set; } = new List<TblManager>();

    public virtual ICollection<TblMember> TblMembers { get; set; } = new List<TblMember>();
}
