using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using newBackendBank.Models;

namespace newBackendBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly OnlineBankingRakeshContext _context;

        public ValuesController(OnlineBankingRakeshContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetDepositMesseges()
        {
            var messeges = _context.DepositRequests;
            return Ok(messeges);
        }

        [HttpPost("Request")]
        public IActionResult Post(long accountNumber , decimal amount)
        {
            DateTime currentDate = DateTime.Now;


            DepositRequest depositRequest = new DepositRequest();

            depositRequest.AccountNumber = accountNumber;
            depositRequest.Amount = amount;
            depositRequest.RecivedDate = currentDate;

            _context.DepositRequests.AddAsync(depositRequest);
            _context.SaveChanges();

            return Ok("Request Successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            var depositRequest = await _context.DepositRequests.FindAsync(id);

            if (depositRequest == null)
            {
                return BadRequest();
            }

            depositRequest.DepositStatus = "successful";

            _context.SaveChanges();


            return Ok("Request Successfully.");
        }



    }
}
