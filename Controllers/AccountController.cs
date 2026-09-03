using Microsoft.AspNetCore.Mvc;
using WebApplication1.Entity;
using System.Security.Cryptography;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class AccountController(DataContext context) : BaseApiController
    {
        // DTO for registration - bind from JSON body
        public sealed record RegisterDto(string UserName, string Password);

        [HttpPost("Register")]
        public async Task<ActionResult<AppUser>> Register([FromBody] RegisterDto request)
        {
            if (request is null || string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Username and password are required.");

            // ensure username is unique
            if (await context.Users.AnyAsync(u => u.UserName == request.UserName))
                return BadRequest("Username is already taken");

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = request.UserName,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.Password)),
                PasswordSalt = hmac.Key
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }
    }
}
