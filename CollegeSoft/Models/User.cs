using System;
using System.Collections.Generic;

namespace CollegeSoft.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? UserEmail { get; set; }

    public string? Upassword { get; set; }

    public string? FullName { get; set; } 

    public string? Phone { get; set; } 

    public string? UserAddress { get; set; }

    public bool? LoginStatus { get; set; }

    public virtual ICollection<Document> Documents { get; } = new List<Document>();

    public virtual ICollection<Fee> FeeCancelledByNavigations { get; } = new List<Fee>();

    public virtual ICollection<Fee> FeeEntryByNavigations { get; } = new List<Fee>();

    public virtual ICollection<FeePrint> FeePrints { get; } = new List<FeePrint>();

    public virtual ICollection<ProgramInfo> ProgramInfoCancelleds { get; } = new List<ProgramInfo>();

    public virtual ICollection<ProgramInfo> ProgramInfoUsers { get; } = new List<ProgramInfo>();

    public virtual ICollection<Reception> ReceptionCancelledBies { get; } = new List<Reception>();

    public virtual ICollection<Reception> ReceptionUsers { get; } = new List<Reception>();

    public virtual ICollection<Student> Students { get; } = new List<Student>();

    public virtual ICollection<Teacher> Teachers { get; } = new List<Teacher>();

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
