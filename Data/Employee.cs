using System;
using System.Collections.Generic;

namespace TECHNOVA.Data;

public partial class Employee
{
    public string EmployeeId { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Qandum> Qanda { get; set; } = new List<Qandum>();

    public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();
}
