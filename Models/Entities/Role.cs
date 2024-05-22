using System;
using System.Collections.Generic;

namespace huynhkimthang_0145_Final_LTC_.Models.Entities;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public string? RoleDecs { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
