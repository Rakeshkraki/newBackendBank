using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newBackendBank.Models;
using System.Linq;
using System.Threading.Tasks;

namespace newBackendBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly OnlineBankingRakeshContext _context;

        public AdminController(OnlineBankingRakeshContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            return await _context.Admins.ToListAsync();
        }

        [HttpGet("emailpass")]
        public async Task<ActionResult<Admin>> GetAdmin(string email, string password)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(res => res.AdminEmail == email);

            if (admin == null)
            {
                return NotFound();
            }

            if (admin.AdminPassword != password)
            {
                return BadRequest();
            }

            return Ok(admin);
        }


        [HttpPost]
        public async Task<ActionResult<Admin>> CreateAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return Ok();
        }


       

    }
}
