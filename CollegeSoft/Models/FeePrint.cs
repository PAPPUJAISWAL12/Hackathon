using System;
using System.Collections.Generic;

namespace CollegeSoft.Models;

public partial class FeePrint
{
    public int PrintId { get; set; }

    public DateTime PrintDate { get; set; }

    public string PrintTime { get; set; } = null!;

    public int PrintUserId { get; set; }

    public int DetailId { get; set; }

    public virtual FeeDetail Detail { get; set; } = null!;

    public virtual User PrintUser { get; set; } = null!;
}
