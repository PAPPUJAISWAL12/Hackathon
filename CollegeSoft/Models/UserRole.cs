using System;
using System.Collections.Generic;

namespace CollegeSoft.Models;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public virtual RoleList Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
