using System;
using System.Collections.Generic;

namespace TECHNOVA.Data;

public partial class Qandum
{
    public int QandAid { get; set; }

    public string Question { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public DateOnly SubmitDate { get; set; }

    public string EmployeeId { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
