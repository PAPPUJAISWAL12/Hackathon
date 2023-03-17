using System;
using System.Collections.Generic;

namespace CollegeSoft.Models;

public partial class UserRoleView
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public int HasRole { get; set; }
}
