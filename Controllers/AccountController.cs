using Microsoft.AspNetCore.Mvc;
using WebApplication1.Entity;
using System.Security.Cryptography;

namespace WebApplication1.Controllers
{
    public class AccountController : BaseApiController
    {
        [HttpPost("Register")]
        public async Task<ActionResult<AppUser>> Register(string username, string password)
        {
            using var hmac = new HMACSHA512();
        
            return Ok();
        }
    }
}
