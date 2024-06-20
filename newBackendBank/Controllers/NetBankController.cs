using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using newBackendBank.Models;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class NetBankController : ControllerBase
{
    private readonly OnlineBankingRakeshContext _context;

    public NetBankController(OnlineBankingRakeshContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var netUsers = _context.UserNetBankDetails.ToList();
        return Ok(netUsers);
    }

    [HttpGet("pan/{pan}")]
    public IActionResult GetUserByPan(string pan)
    {
        var userDetail = _context.UserDetails.SingleOrDefault(u => u.Pan == pan);
        if (userDetail == null)
        {
            return NotFound(new { message = "User with given PAN not found." });
        }
        return Ok(userDetail);
    }

    [HttpGet("NetBankUser/{pan}")]
    public IActionResult GetNetUserByPan(string pan)
    {
        var userDetail = _context.UserNetBankDetails.SingleOrDefault(u => u.Pan == pan);
        if (userDetail == null)
        {
            return NotFound(new { message = "User with given PAN not found." });
        }
        return Ok(userDetail);
    }

    [HttpPost]
    public IActionResult Post([FromBody] UserNetBankDetail netUser)
    {
        if (netUser == null)
        {
            return BadRequest("Invalid user data.");
        }

        var account = _context.UserDetails.FirstOrDefault(user => user.Pan == netUser.Pan);
        if (account == null)
        {
            return BadRequest("PAN not found.");
        }

        var generatedDetails = _context.GeneratedDetails.FirstOrDefault(id => id.UserId == account.UserDetailsId);

        if (generatedDetails == null)
        {
            return BadRequest("Generated details not found.");
        }

        if (netUser.AccountNumber != generatedDetails.AccountNumber)
        {
            return BadRequest("Invalid Account Number.");
        }

        if (netUser.MobileNumber != account.MobileNumber)
        {
            return BadRequest("Invalid Mobile Number.");
        }

        netUser.UserNetBankId = account.UserDetailsId;

        _context.UserNetBankDetails.Add(netUser);
        _context.SaveChanges();

        return Ok("User registered successfully for netbanking.");
    }


    [HttpGet("login")]
    public IActionResult Login(string pan, string password)
    {
        var user = _context.UserNetBankDetails.FirstOrDefault(u => u.Pan == pan);

        if (user == null)
        {
            return BadRequest("Invalid PAN Number");
        }

        if(user.Password != password)
        {
            return BadRequest("Invalid Password");
        }

        return Ok(user);

    }

   

   

}
