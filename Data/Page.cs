﻿using System;
using System.Collections.Generic;

namespace TECHNOVA.Data;

public partial class Page
{
    public int PageId { get; set; }

    public string PageName { get; set; } = null!;

    public string Url { get; set; } = null!;

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
