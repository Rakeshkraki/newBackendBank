using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newBackendBank.Models;


namespace BackendBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateAccountController : ControllerBase
    {
        OnlineBankingRakeshContext db = new OnlineBankingRakeshContext();

        private readonly GeneratedDetailsService _generatedDetailsService;

        public CreateAccountController(GeneratedDetailsService generatedDetailsService)
        {
            _generatedDetailsService = generatedDetailsService;
        }

        [HttpGet]
        public IActionResult GetAccountants()
        {
            var users = db.UserDetails;
            return Ok(users);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDetail value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var existUser =await db.UserDetails.FirstOrDefaultAsync(res => res.Pan == value.Pan);

            if (existUser != null)
            {
                return BadRequest("User with Pan Number already Exists");
            }

            db.UserDetails.Add(value);

            await db.SaveChangesAsync();

            var userId = value.UserDetailsId;

            int generatedId = await _generatedDetailsService.AddGeneratedDetails(userId, value.SelectedBranch);

            return Ok(new { UserDetailsId = userId, GeneratedId = generatedId });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userDetail = await db.UserDetails.FindAsync(id);

            if (userDetail == null)
            {
                return NotFound();
            }

            db.UserDetails.Remove(userDetail);
            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("pan")]

        public async Task<IActionResult> getByPanNumber(string pan)
        {
            if(pan == null)
            {
                return NoContent();
            }
            var user = await db.UserDetails.FirstOrDefaultAsync(u => u.Pan == pan);

            if (user == null)
            {
                return BadRequest("User Not found");
            }

            return Ok(user);
        }

    }


}
