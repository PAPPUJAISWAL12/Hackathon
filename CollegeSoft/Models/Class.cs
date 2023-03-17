using System;
using System.Collections.Generic;

namespace CollegeSoft.Models;

public partial class Class
{
    public int Cid { get; set; }

    public string Cname { get; set; } = null!;

    public virtual ICollection<Student> Students { get; } = new List<Student>();

    public virtual ICollection<Subject> Subjects { get; } = new List<Subject>();
}
