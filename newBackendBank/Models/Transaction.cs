using System;
using System.Collections.Generic;

namespace newBackendBank.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public string TransactionType { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateOnly TransactionDate { get; set; }

    public long FromAccount { get; set; }

    public long ToAccount { get; set; }
}
