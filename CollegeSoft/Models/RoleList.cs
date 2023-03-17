using System;
using System.Collections.Generic;

namespace CollegeSoft.Models;

public partial class RoleList
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
