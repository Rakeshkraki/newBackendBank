using System;
using System.Collections.Generic;

namespace newBackendBank.Models;

public partial class Branch
{
    public int BranchId { get; set; }

    public string BranchName { get; set; } = null!;

    public string BranchCode { get; set; } = null!;

    public string BranchAddress { get; set; } = null!;

    public string Phone { get; set; } = null!;
}
