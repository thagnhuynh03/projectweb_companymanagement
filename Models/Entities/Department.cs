using System;
using System.Collections.Generic;

namespace huynhkimthang_0145_Final_LTC_.Models.Entities;

public partial class Department
{
    public int DepId { get; set; }

    public string? DepName { get; set; }

    public string? DepDesc { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
