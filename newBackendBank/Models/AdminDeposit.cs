using System;
using System.Collections.Generic;

namespace newBackendBank.Models;

public partial class AdminDeposit
{
    public int DepositId { get; set; }

    public long DepositToAccount { get; set; }

    public decimal DepositAmmount { get; set; }

    public DateTime DepositDate { get; set; }
}
