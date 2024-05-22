using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace huynhkimthang_0145_Final_LTC_.Models.Entities;

public partial class Employee
{
    public int EmpId { get; set; }

    public string? EmpName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
    [RegularExpression(@"^(090|098|091|031|035|038)\d{7}$", ErrorMessage = "Phone number must be a 10-digit number starting with 090, 098, 091, 031, 035, or 038.")]
    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public int? Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Avartar { get; set; }

    public int? DepId { get; set; }

    public int? RoleId { get; set; }

    public virtual Department? Dep { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual Role? Role { get; set; }
}
