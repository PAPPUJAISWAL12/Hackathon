using System;
using System.Collections.Generic;

namespace CollegeSoft.Models;

public partial class SubjectsView
{
    public int SubId { get; set; }

    public string SubName { get; set; } = null!;

    public int Cid { get; set; }

    public string Cname { get; set; } = null!;
}
