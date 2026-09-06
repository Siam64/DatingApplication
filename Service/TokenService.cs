using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Entity;
using WebApplication1.Interface;

namespace WebApplication1.Service
{
    public class TokenService(IConfiguration config) : ITokenService
    {
        public string CreateToken(AppUser user)
        {
            var tokenKey = config["TokenKey"] ?? throw new InvalidOperationException("Cannot access TokenKey from appsettings.");
            if(tokenKey.Length < 64) throw new InvalidOperationException("TokenKey must be at least 64 characters long.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.UserName) 
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
