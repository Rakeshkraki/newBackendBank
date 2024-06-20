using System;
using System.Collections.Generic;

namespace newBackendBank.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string AdminName { get; set; } = null!;

    public string AdminEmail { get; set; } = null!;

    public long AdminPhone { get; set; }

    public string AdminPassword { get; set; } = null!;
}
