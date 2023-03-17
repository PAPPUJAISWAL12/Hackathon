using System;
using System.Collections.Generic;

namespace CollegeSoft.Models;

public partial class Student
{
    public int StdId { get; set; }

    public int Cid { get; set; }

    public int UserId { get; set; }

    public virtual Class CidNavigation { get; set; } = null!;

    public virtual ICollection<Fee> Fees { get; } = new List<Fee>();

    public virtual User User { get; set; } = null!;
}
