using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newBackendBank.Models;

namespace newBackendBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneratedValuesController : ControllerBase
    {

        private readonly OnlineBankingRakeshContext _context;

        public GeneratedValuesController(OnlineBankingRakeshContext context)
        {
            _context = context;
        }

        [HttpGet("Pan")]

        public async Task<IActionResult> GetGeneratedValues(string pan)
        {

            var GenDetails =await  _context.GeneratedDetails.FirstOrDefaultAsync(res => res.Pan == pan); 

            return Ok(GenDetails);
        }



        



    }
}
