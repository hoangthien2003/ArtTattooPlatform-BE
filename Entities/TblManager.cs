﻿using System;
using System.Collections.Generic;

namespace back_end.Entities;

public partial class TblManager
{
    public int ManagerId { get; set; }

    public string? ManagerName { get; set; }

    public string? ManagerPhone { get; set; }

    public int? UserId { get; set; }

    public int? StudioId { get; set; }

    public virtual TblStudio? Studio { get; set; }

    public virtual TblUser? User { get; set; }
}
