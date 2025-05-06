using System;
using System.Collections.Generic;

namespace TECHNOVA.Data;

public partial class Permission
{
    public int PermissionId { get; set; }

    public string? DepartmentId { get; set; }

    public int? PageId { get; set; }

    public bool CanAdd { get; set; }

    public bool CanEdit { get; set; }

    public bool CanDelete { get; set; }

    public bool CanView { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Page? Page { get; set; }
}
