using WebApplication1.Entity;
using WebApplication1.Interface;

namespace WebApplication1.Service
{
    public class TokenService : ITokenService
    {
        public string CreateToken(AppUser user)
        {
            // Implementation for creating token
            return "sample_token";
        }
    }
}
