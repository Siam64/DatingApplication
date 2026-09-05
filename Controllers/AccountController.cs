using Microsoft.AspNetCore.Mvc;
using WebApplication1.Entity;
using System.Security.Cryptography;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entity.DTOs;

namespace WebApplication1.Controllers
{
    public class AccountController(DataContext context) : BaseApiController
    {
        // DTO for registration - bind from JSON body
        //public sealed record RegisterDto(string UserName, string Password);

        [HttpPost("Register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto request)
        {
            if (request is null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Username and password are required.");

            // ensure username is unique
            if (await context.Users.AnyAsync(u => u.UserName == request.Username))
                return BadRequest("Username is already taken");

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = request.Username,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.Password)),
                PasswordSalt = hmac.Key
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AppUser>> Login(LoginDto Loginrequest)
        {
            if (Loginrequest is null || string.IsNullOrWhiteSpace(Loginrequest.Username) || string.IsNullOrWhiteSpace(Loginrequest.Password))
                return BadRequest("Username and password are required.");

            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == Loginrequest.Username.ToLower());
            if (user is null)
                return Unauthorized("Invalid username or password");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Loginrequest.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("Invalid username or password");
            }

            return Ok(new { message = "Login successful", user });
        }
    }
}
