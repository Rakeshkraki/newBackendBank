using System;
using System.Collections.Generic;

namespace newBackendBank.Models;

public partial class GeneratedDetail
{
    public int GeneratedId { get; set; }

    public int UserId { get; set; }

    public string? IfseCode { get; set; }

    public string? BranchName { get; set; }

    public long AccountNumber { get; set; }

    public string? Pan { get; set; }

    public virtual UserDetail User { get; set; } = null!;
}
