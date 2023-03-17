using System;
using System.Collections.Generic;

namespace CollegeSoft.Models;

public partial class TeacherView
{
    public int Tid { get; set; }

    public int UserId { get; set; }

    public string Tpost { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string UserAddress { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public bool LoginStatus { get; set; }
}
