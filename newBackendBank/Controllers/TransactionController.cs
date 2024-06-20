using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using newBackendBank.Models;

namespace newBackendBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly GeneratedDetailsService _generatedDetailsService;

        private readonly OnlineBankingRakeshContext _context;

        public TransactionController(OnlineBankingRakeshContext context , GeneratedDetailsService generatedDetailsService)
        {
            _context = context;
            _generatedDetailsService = generatedDetailsService;

        }

    


        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult> GetTransactions()
        {
            var transactions = _context.Transactions;
            return Ok(transactions);
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }



        // POST: api/Transactions
        [HttpPost]
        public async Task<ActionResult> PostTransaction(long fromAccount, long toAccount, decimal amount  )
        {
            if (fromAccount == 0 || toAccount == 0 || amount == 0)
            {
                return Ok();
            }
           

            var fromUser = await _context.UserNetBankDetails.FirstOrDefaultAsync(u => u.AccountNumber == fromAccount);

            if(fromUser.Balance < amount)
            {
                return BadRequest("Insufficient Balance");
            }

            var toUser = await _context.UserNetBankDetails.FirstOrDefaultAsync(u => u.AccountNumber == toAccount);

            if (toUser == null)
            {
                return BadRequest("User Not Found");
            }

            fromUser.Balance -= amount;
            toUser.Balance += amount;

            Transaction transaction = new Transaction();

            DateTime currentDate = DateTime.Now;

            transaction.TransactionDate = DateOnly.FromDateTime(currentDate);
            transaction.FromAccount = fromAccount;
            transaction.ToAccount = toAccount;
            transaction.Amount = amount;
            transaction.TransactionType = "online";

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return Ok(transaction);
        }

        [HttpGet("Sent/{accountNumber}")]
        public async Task<IActionResult> GetTransactionsByFromAccountNumber(long accountNumber)
        {
            try
            {
                List<Transaction> transactions = await _generatedDetailsService.GetTransactionsByFromAccountNumberAsync(accountNumber);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("Recived/{accountNumber}")]
        public async Task<IActionResult> GetTransactionsToAccountNumber(long accountNumber)
        {
            try
            {
                List<Transaction> transactions = await _generatedDetailsService.GetTransactionsToAccountNumberAsync(accountNumber);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost("Deposit")]
        public async Task<ActionResult> PostDeposit(long accountNum, decimal amount)
        {
            if (accountNum == 0 || amount == 0)
            {
                return BadRequest();
            }



            var toUser = await _context.UserNetBankDetails.FirstOrDefaultAsync(u => u.AccountNumber == accountNum);

            if (toUser == null)
            {
                return BadRequest("User Not Found");
            }

            toUser.Balance += amount;

            AdminDeposit transaction = new AdminDeposit();

            DateTime currentDate = DateTime.Now;

            transaction.DepositDate = currentDate;
            transaction.DepositToAccount = accountNum;
            transaction.DepositAmmount = amount;

            _context.AdminDeposits.Add(transaction);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("GetAllDeposits")]
        public async Task<IActionResult> GetDeposits()
        {
            var deposits = _context.AdminDeposits;
            return Ok(deposits);
        }


        [HttpGet("dateWise")]

        public async Task<IActionResult> getTransactionsByDates()
        {
            var trans =  await _context.DailyTrans.FromSqlRaw("EXEC GetDailyTransactionCount ").ToListAsync();

            return Ok(trans);
        }

    }
}