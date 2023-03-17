using System;
using System.Collections.Generic;

namespace CollegeSoft.Models;

public partial class Teacher
{
    public int Tid { get; set; }

    public int UserId { get; set; }

    public string Tpost { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
