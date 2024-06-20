using System;
using System.Collections.Generic;

namespace newBackendBank.Models;

public partial class DepositRequest
{
    public int MessegeId { get; set; }

    public long AccountNumber { get; set; }

    public decimal Amount { get; set; }

    public DateTime? RecivedDate { get; set; }

    public string? DepositStatus { get; set; }
}
