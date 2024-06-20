using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using newBackendBank.Models;

public class GeneratedDetailsService
{
    private readonly OnlineBankingRakeshContext _dbContext;

    public GeneratedDetailsService(OnlineBankingRakeshContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> AddGeneratedDetails(int userId, int? selectedBranch)
    {
        long accountNumber = GenerateUniqueAccountNumber();

        var branch = _dbContext.Branches.Find(selectedBranch);
        string ifscCode = branch.BranchCode; 
        string branchName = branch.BranchName;
        var user = _dbContext.UserDetails.Find(userId);

        var generatedDetails = new GeneratedDetail
        {
            UserId = userId,
            AccountNumber = accountNumber,
            IfseCode = ifscCode,
            BranchName = branchName,
            Pan = user.Pan
            
        };

        _dbContext.GeneratedDetails.Add(generatedDetails);
        await _dbContext.SaveChangesAsync();

        return generatedDetails.GeneratedId;
    }

    private long GenerateUniqueAccountNumber()
    {
        Random random = new Random();
        long accountNumber = random.NextInt64(100000000000, 999999999999L); 

        while (_dbContext.GeneratedDetails.Any(g => g.AccountNumber == accountNumber))
        {
            accountNumber = random.NextInt64(100000000000, 999999999999L);
        }

        return accountNumber;
    }



    public async Task<List<Transaction>> GetTransactionsByFromAccountNumberAsync(long accountNumber)
    {
        return await _dbContext.Transactions
            .FromSqlRaw("EXEC GetTransactionsByFromAccountNumber @FromAccountNumber",
                        new SqlParameter("@FromAccountNumber", accountNumber))
            .ToListAsync();
    }

    public async Task<List<Transaction>> GetTransactionsToAccountNumberAsync(long accountNumber)
    {
        return await _dbContext.Transactions
            .FromSqlRaw("EXEC GetTransactionsToAccountNumber @FromAccountNumber",
                        new SqlParameter("@FromAccountNumber", accountNumber))
            .ToListAsync();
    }



}
