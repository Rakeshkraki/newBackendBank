using System;
using System.Collections.Generic;

namespace newBackendBank.Models;

public partial class UserNetBankDetail
{
    public int UserNetBankId { get; set; }

    public string Password { get; set; } = null!;

    public long AccountNumber { get; set; }

    public string Pan { get; set; } = null!;

    public string MobileNumber { get; set; } = null!;

    public decimal? Balance { get; set; }
}
